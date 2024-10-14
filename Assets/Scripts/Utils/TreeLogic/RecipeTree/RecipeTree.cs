using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RecipeTree/RecipeTree")]
public class RecipeTree : ScriptableObject
{
    [SerializeField] private RecipeNode _rootNode;
    [SerializeField] private RecipeNode GetRootNode() => _rootNode;

    public List<RecipeNode> GetRawIngredients()
    {
        List<RecipeNode> ingredients = new List<RecipeNode>();
        GetRawIngredients(_rootNode, ingredients);
        return ingredients;
    }

    public List<RecipeNode> GetFoodByStation(Station station)
    {
        List<RecipeNode> ingredients = new List<RecipeNode>();
        GetFoodByStation(_rootNode, ingredients, station);
        return ingredients;
    }

    private static void GetRawIngredients(RecipeNode _recipeNode, List<RecipeNode> ingredients)
    {
        if (_recipeNode.GetChildrenNodes.Count == 0) ingredients.Add(_recipeNode);
        else foreach (RecipeNode rn in _recipeNode.GetChildrenNodes)
            {
                GetRawIngredients(rn, ingredients);
            }
    }

    private static void GetFoodByStation(RecipeNode _recipeNode, List<RecipeNode> ingredients, Station station)
    {
        if (_recipeNode.GetStation() == station)
            ingredients.Add(_recipeNode);

        foreach (RecipeNode rn in _recipeNode.GetChildrenNodes)
        {
            GetFoodByStation(rn, ingredients, station);
        }
    }
}
