using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Food: MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private List<GameObject> _ingredientList = new List<GameObject>();
    [SerializeField] private GameObject _failedFood;
    [SerializeField] private CookingSettings _cooking;

    private void Start() => _ingredientList = _ingredientList.OrderBy(go => go.name).ToList();

    public List<GameObject> GetRawIngredients()
    {
        List<GameObject> ingredients = new List<GameObject>();
        GetRawIngredients(ingredients);
        return ingredients;
    }

    public GameObject GetFailedFood() => _failedFood;

    public List<GameObject> GetIngredientList() => _ingredientList;

    private void GetRawIngredients(List<GameObject> ingredients)
    {
        if (_ingredientList.Count == 0) ingredients.Add(gameObject);
        else foreach (GameObject go in _ingredientList)
            {
                Food food = go.GetComponent<Food>();
                food.GetRawIngredients(ingredients);
            }
    }
}
