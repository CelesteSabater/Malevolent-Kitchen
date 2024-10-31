using System.Collections.Generic;
using System.Linq;
using Celeste.Tools.RecipeTree;
using UnityEngine;
using UnityEngine.UI;

public class CookingManager : Singleton<CookingManager>
{
    [Header("Cooking Settings")]
    [SerializeField] private List<RecipeTree> _menu = new List<RecipeTree>();
    [SerializeField] private int _maxStrikes = 3;
    [SerializeField] private List<GameObject> _foodSpawnPos = new List<GameObject>();

    private int _currentStrikes = 0;
    private RecipeTree _currentRequest;
    private RecipeTree _lastRequest;

    public void InitializeRandomRequest()
    {
        GameManager.Instance.DestroyAllFood(false);

        RecipeTree tryRequest;
        do
        {
            tryRequest = GetRandomRequest();
        } while (_lastRequest == tryRequest);

        _lastRequest = _currentRequest;
        _currentRequest = tryRequest;
        InstanceIngredients(_currentRequest.GetRootNode());
    }

    private RecipeTree GetRandomRequest()
    {
        int i = Random.Range(0, _menu.Count);
        return _menu[i];
    }

    public void InstanceIngredients(RecipeNode node)
    {
        List<RecipeNode> ingredients = RecipeTree.GetNodes(node, typeof(IngredientNode));

        int i = 0;
        foreach (RecipeNode ingredient in ingredients)
        {
            GameManager.Instance.SpawnFood(ingredient.GetFoodData(), _foodSpawnPos[i].transform.position, false);
            i++;
        }
    }

    public void InstanceIngredients(Food food)
    {
        int i = 0;
        foreach (FoodData data in food.GetChildren())
        {
            GameManager.Instance.SpawnFood(data, _foodSpawnPos[i].transform.position, false);
            i++;
        }
    }

    public List<RecipeNode> GetRecipes(StationType station)
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
                case StationType.MixingStation:
                    List<RecipeNode> mixingNode = RecipeTree.GetNodes(recipe.GetRootNode(), typeof(MixingNode));
                    List<RecipeNode> rootNode = RecipeTree.GetNodes(recipe.GetRootNode(), typeof(RecipeRoot));
                    list = new List<RecipeNode>(mixingNode.Count + rootNode.Count);
                    list.AddRange(mixingNode);
                    list.AddRange(rootNode);
                    break;
            }

           nodesInStation = nodesInStation.Concat(list).ToList();
        }

        return nodesInStation;
    }

    public List<Image> GetCookbook()
    {
        List<Image> cookbook = new List<Image>();
        foreach(RecipeTree recipe in _menu)
        {
            cookbook.Concat(recipe.GetRecipe());
        }
        return cookbook;
    }

    public bool GameIsLost() => _currentStrikes < _maxStrikes;
}
