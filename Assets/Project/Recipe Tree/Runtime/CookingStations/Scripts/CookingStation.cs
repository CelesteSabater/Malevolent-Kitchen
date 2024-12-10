using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Project.RecipeTree.Runtime;
using UnityEditor;

public class CookingStation : MonoBehaviour
{
    [Header("Game Data")]
    [SerializeField] private GameObject _foodSpawnPos;
    [SerializeField] private RecipeNode _currentRecipe;
    [SerializeField] private List<RecipeNode> _stationRecipes = new List<RecipeNode>();  
    [SerializeField] private Dictionary<string, GameObject> _ingredientsOnArea = new Dictionary<string, GameObject>();

    private string _stationGuid;

    #region Getters and Setters
    public RecipeNode GetCurrentRecipe() => _currentRecipe;
    public string GetStationGuid() => _stationGuid;
    public Vector3 GetFoodSpawnPos() => _foodSpawnPos.transform.position;
    #endregion

    protected virtual void Start()
    {
        _stationGuid = GUID.Generate().ToString();
        _stationRecipes = CookingManager.Instance.GetRecipes(this);
        RestartData();
        GameEvents.current.onRecipeStep += OnRecipeStep;
    }

    protected virtual void OnDestroy() 
    {
        GameEvents.current.onRecipeStep -= OnRecipeStep;
    }

    private List<RecipeNode> GetChildList(RecipeNode node)
    {
        List<RecipeNode> ingredients = new List<RecipeNode>();
        switch(node)
        {
            case CuttingNode _node:
                ingredients.Add(_node._child); 
                break;
            case FryingNode _node:
                ingredients = _node._children;
                break;
            case FurnaceNode _node:
                ingredients = _node._children;
                break;
            case PotNode _node:
                ingredients = _node._children;
                break;
            case MixingNode _node:
                ingredients = _node._children;
                break;
        }
        return ingredients;
    }

    protected virtual void CheckIngredients()
    {
        _currentRecipe = null;

        foreach (RecipeNode recipe in _stationRecipes)
        {
            List<RecipeNode> ingredients = GetChildList(recipe);
            if (EqualList(ingredients, _ingredientsOnArea))
            {
                RestartData();
                _currentRecipe = recipe;

                foreach (string key in _ingredientsOnArea.Keys.ToList())
                {
                    FoodSpawnManager.Instance.DestroyFood(_ingredientsOnArea[key], false);
                    _ingredientsOnArea.Remove(key);
                }

                break;
            }
        }
    }

    private bool EqualList(List<RecipeNode> recipe, Dictionary<string, GameObject> ingredientList)
    {
        if (recipe.Count != ingredientList.Count)
            return false;

        foreach (RecipeNode ingredient in recipe)
            if (!ingredientList.ContainsKey(ingredient.GetFoodName())) 
                return false;

        return true;
    }

    public void PopCurrentRecipe()
    {
        if(_currentRecipe == null)
            return;

        CookingManager.Instance.InstanceIngredients(_currentRecipe);
        _currentRecipe = null;
    }

    protected virtual void OnRecipeStep(string stationGuid)
    {
        if (stationGuid != _stationGuid)
            return;
        
        if (_currentRecipe == null)
            return;     

        RecipeRoot root = _currentRecipe as RecipeRoot;
        if (root != null)
            AudioSystem.Instance.PlayMusicWithDelay(FoodSpawnManager.Instance.GetSummonDelay(),"WeDidIt");

        bool FOOD_IS_NOT_BURNT = false;
        FoodSpawnManager.Instance.InstanceFood(_currentRecipe.GetFoodData(), GetFoodSpawnPos(), FOOD_IS_NOT_BURNT);
        RestartData();   
    }

    protected virtual void RestartData()
    {
        _currentRecipe = null;
        UpdateVisuals.UpdateCookingStation(this);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (_currentRecipe != null)
            return;

        if (!other.GetComponent<Food>())
            return;

        if (other.GetComponent<Food>().GetIsBurnt())
        return;

        _ingredientsOnArea[other.GetComponent<Food>().GetName()] = other.gameObject;
        CheckIngredients();
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (_currentRecipe != null)
        return;

        if (!other.GetComponent<Food>())
            return;

        if (other.GetComponent<Food>().GetIsBurnt())
        return;

        _ingredientsOnArea.Remove(other.GetComponent<Food>().GetName());
        CheckIngredients();
    }
}