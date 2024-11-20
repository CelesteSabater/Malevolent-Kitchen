using System;
using System.Collections.Generic;
using System.Linq;
using Project.RecipeTree.Runtime;
using UnityEngine;
using UnityEngine.UI;

public enum Difficulty
{
    Easy = 5,
    Medium = 3,
    Hard = 2
}

public class CookingManager : Singleton<CookingManager>
{
    [Header("Cooking Settings")]
    [SerializeField] private List<RecipeTree> _menu = new List<RecipeTree>();
    [SerializeField] private int _maxStrikes = 3;
    [SerializeField] private float _difficultyModifier;
    [SerializeField] private List<GameObject> _foodSpawnPos = new List<GameObject>();

    [Header("Difficulty Settings")]
    [SerializeField] private Difficulty _difficulty;
    [SerializeField] private int _completedRecipes;

    private int _currentStrikes = 0;
    private RecipeTree _currentRequest;
    private RecipeTree _lastRequest;

    void Start()
    {
        InitializeRandomRequest();
        GameEvents.current.onStrike += OnStrike;
        GameEvents.current.onCompleteFood += OnCompleteFood;
    }

    private void OnDestroy() {
        GameEvents.current.onStrike -= OnStrike;
        GameEvents.current.onCompleteFood -= OnCompleteFood;
    }

    public void InitializeRandomRequest()
    {
        FoodSpawnManager.Instance.DestroyAllFood(false);

        RecipeTree tryRequest;
        do
        {
            tryRequest = GetRandomRequest();
        } while (_lastRequest == tryRequest);

        _lastRequest = _currentRequest;
        _currentRequest = tryRequest;
        
        GameEvents.current.NewRecipe(_currentRequest.GetRecipeName());
        InstanceIngredients(_currentRequest.GetRootNode());
    }

    private RecipeTree GetRandomRequest()
    {
        int i = UnityEngine.Random.Range(0, _menu.Count);
        return _menu[i];
    }

    public void InstanceIngredients(RecipeNode node)
    {
        List<RecipeNode> ingredients = RecipeTree.GetNodes(node, typeof(IngredientNode));

        int i = 0;
        foreach (RecipeNode ingredient in ingredients)
        {
            FoodSpawnManager.Instance.InstanceFood(ingredient.GetFoodData(), _foodSpawnPos[i].transform.position, false);
            i++;
        }
    }

    public void InstanceIngredients(Food food)
    {
        int i = 0;
        foreach (FoodData data in food.GetChildren())
        {
            FoodSpawnManager.Instance.InstanceFood(data, _foodSpawnPos[i].transform.position, false);
            i++;
        }
    }

    [HideInInspector] public List<RecipeNode> GetRecipes(StationType station)
    {
        List<RecipeNode> nodesInStation = new List<RecipeNode>();

        foreach (RecipeTree recipe in _menu)
        {
            List<RecipeNode> list = null;
            switch (station)
            {
                case StationType.CuttingStation:
                    list = RecipeTree.GetNodes(recipe.GetRootNode(), typeof(CuttingNode));
                    break;
                case StationType.FryingStation:
                    list = RecipeTree.GetNodes(recipe.GetRootNode(), typeof(FryingNode));
                    break;
                case StationType.FurnaceStation:
                    list = RecipeTree.GetNodes(recipe.GetRootNode(), typeof(FurnaceNode));
                    break;
                case StationType.PotStation:
                    list = RecipeTree.GetNodes(recipe.GetRootNode(), typeof(PotNode));
                    break;
                case StationType.MixingStation:
                    List<RecipeNode> mixingNode = RecipeTree.GetNodes(recipe.GetRootNode(), typeof(MixingNode));
                    List<RecipeNode> rootNode = RecipeTree.GetNodes(recipe.GetRootNode(), typeof(RecipeRoot));
                    list = new List<RecipeNode>(mixingNode.Count + rootNode.Count);
                    list.AddRange(mixingNode);
                    list.AddRange(rootNode);
                    break;
            }
            list = list.Distinct().ToList();
            nodesInStation = nodesInStation.Concat(list).ToList();
        }

        return nodesInStation;
    }

    public Sprite[] GetCookbook()
    {
        Sprite[] cookbook = new Sprite[0];
        foreach(RecipeTree recipe in _menu)
        {
            Sprite[] newList = recipe.GetRecipe();
            Sprite[] sum = new Sprite[cookbook.Length + newList.Length];
            cookbook.CopyTo(sum, 0);
            newList.CopyTo(sum, cookbook.Length);
            cookbook = sum;
        }
        return cookbook;
    }

    public float GetRecipeTime()
    {
        throw new System.NotImplementedException();
        // tiempo ideal receta * multiplicador dificultad / modificador de recetas completadas
        return (1 * (int)_difficulty) / (1 + _completedRecipes * 0.1f);
    }

    private void OnStrike() => _currentStrikes++;
    private void OnCompleteFood() => _completedRecipes++;

    public bool GameIsLost() => _currentStrikes < _maxStrikes;
}