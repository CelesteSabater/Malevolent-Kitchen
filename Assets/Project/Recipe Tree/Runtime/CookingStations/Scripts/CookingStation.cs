using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Project.RecipeTree.Runtime;
using UnityEditor;

public enum StationType
{
    CuttingStation,
    FryingStation,
    FurnaceStation,
    PotStation,
    MixingStation
}

public class CookingStation : MonoBehaviour
{
    [Header("Game Data")]
    [SerializeField] private GameObject _foodSpawnPos;
    [SerializeField] private StationType _stationType;
    [SerializeField] private RecipeNode _currentRecipe;
    [SerializeField] private List<RecipeNode> _stationRecipes = new List<RecipeNode>();  
    [SerializeField] private Dictionary<string, GameObject> _ingredientsOnArea = new Dictionary<string, GameObject>();

    [Header("Visual Effects")]
    [SerializeField] private GameObject _exclamationIcon;
    [SerializeField] private GameObject _tool;
    [SerializeField] private TimerController _timer;
    [SerializeField] private ParticleSystem _particleSystem;

    [Header("Particle Settings")]
    [SerializeField] private ParticleSystemData _particleSystemData;

    private bool _stationIsOn = false;
    private bool _foodIsBurnt = false;
    private bool _foodIsReady = false;
    private string _stationGuid;

    public bool GetStationIsOn() => _stationIsOn;
    public void SwitchStationIsOn() =>_stationIsOn = !_stationIsOn;
    public RecipeNode GetCurrentRecipe() => _currentRecipe;
    public bool GetFoodIsBurnt() => _foodIsBurnt;
    public void SetFoodIsBurnt(bool value)
    {
        if (value == _foodIsBurnt)
            return;

        _foodIsBurnt = value;

        if (_foodIsBurnt)
            AudioSystem.Instance.PlaySFX("Burnt",gameObject.transform.position);
    }
    public bool GetFoodIsReady() => _foodIsReady;
    public void SetFoodIsReady(bool value)
    {
        if (value == _foodIsReady)
            return;

        _foodIsReady = value;

        if (_foodIsReady)
            AudioSystem.Instance.PlaySFX("Ding",gameObject.transform.position);
    }
    public GameObject GetTool() => _tool;
    public GameObject GetExclamationIcon() => _exclamationIcon;
    public TimerController GetTimerController() => _timer;
    public ParticleSystem GetParticleSystem() => _particleSystem;
    public ParticleSystemData GetParticleSystemData() => _particleSystemData;
    public string GetStationGuid() => _stationGuid;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            SwitchStationIsOn();    
    }

    private void Start()
    {
        _stationGuid = GUID.Generate().ToString();
        _stationRecipes = CookingManager.Instance.GetRecipes(_stationType);
        RestartData();
        GameEvents.current.onBurnFood += OnBurnFood;
        GameEvents.current.onRecipeStep += OnRecipeStep;
    }

    private void OnDestroy() {
        GameEvents.current.onBurnFood -= OnBurnFood;
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

    private void CheckIngredients()
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

    private void OnRecipeStep(string stationGuid)
    {
        if (stationGuid != _stationGuid)
            return;
        
        if (_currentRecipe == null || !_foodIsReady)
            return;     

        FoodSpawnManager.Instance.InstanceFood(_currentRecipe.GetFoodData(), _foodSpawnPos.transform.position, _foodIsBurnt);
        RestartData();   
    }

    private void OnBurnFood(string stationGuid)
    {
        if (stationGuid != _stationGuid)
            return;
        
        if (_currentRecipe == null || !_foodIsBurnt)
            return;  
        
        FoodSpawnManager.Instance.InstanceFood(_currentRecipe.GetFoodData(), _foodSpawnPos.transform.position, _foodIsBurnt);
        RestartData();
    }

    public void UpdateTimer(float percent) 
    {
        UpdateVisuals.UpdateCookingStation(this);
        _timer.SetFill(percent);
    }

    private void RestartData()
    {
        _currentRecipe = null;
        _stationIsOn = false;
        _foodIsBurnt = false;
        _foodIsReady = false;
        UpdateVisuals.UpdateCookingStation(this);
    }

    private void OnTriggerEnter(Collider other)
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

    private void OnTriggerExit(Collider other)
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