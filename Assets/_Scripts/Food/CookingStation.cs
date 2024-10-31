using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Celeste.Tools.RecipeTree;
 using System.Collections.Generic; 

public enum StationType
{
    CuttingStation,
    FryingStation,
    FurnaceStation,
    MixingStation
}

public class CookingStation : MonoBehaviour
{
    [SerializeField] private GameObject _foodSpawnPos;
    [SerializeField] private StationType _stationType;
    [SerializeField] private RecipeNode _currentRecipe;
    [SerializeField] private List<RecipeNode> _stationRecipes = new List<RecipeNode>();  
    [SerializeField] private Dictionary<string, GameObject> _ingredientsOnArea = new Dictionary<string, GameObject>();

    private void Start()
    {
        List<RecipeNode> _stationRecipes = CookingManager.Instance.GetRecipes(_stationType);
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
            case MixingNode _node:
                ingredients = _node._children;
                break;
        }
        return ingredients;
    }

    private void CheckIngredients()
    {
        _currentRecipe = null;

        foreach (RecipeNode recipe in _stationRecipes)
        {
            List<RecipeNode> ingredients = GetChildList(recipe);
            if (EqualList(ingredients, _ingredientsOnArea))
            {
                _currentRecipe = recipe;
                foreach (string key in _ingredientsOnArea.Keys)
                {
                    GameManager.Instance.DestroyFood(_ingredientsOnArea[key], false);
                    _ingredientsOnArea.Remove(key);
                }
                    
                Debug.Log($"Nueva receta: {recipe.GetFoodName()}");
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
        CookingManager.Instance.InstanceIngredients(_currentRecipe);
        _currentRecipe = null;
    }

    public void CompleteCookingChallenge()
    {
        if (_currentRecipe == null)
            return;
        
        _currentRecipe = null;
        GameManager.Instance.SpawnFood(_currentRecipe.GetFoodData(), _foodSpawnPos.transform.position, false);
    }

    public void FailCookingChallenge()
    {
        if (_currentRecipe == null)
            return;

        _currentRecipe = null;
        GameManager.Instance.SpawnFood(_currentRecipe.GetFoodData(), _foodSpawnPos.transform.position, true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Food>()) _ingredientsOnArea[other.GetComponent<Food>().GetName()] = other.gameObject;
        CheckIngredients();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Food>()) _ingredientsOnArea.Remove(other.GetComponent<Food>().GetName());
        CheckIngredients();
    }
}
