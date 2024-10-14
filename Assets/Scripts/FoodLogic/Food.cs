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

    public List<GameObject> GetFoodByStation(Station station)
    {
        List<GameObject> ingredients = new List<GameObject>();
        GetFoodByStation(ingredients, station);
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

    private void GetFoodByStation(List<GameObject> ingredients, Station station)
    {
        if (_cooking != null)
            if (_cooking._station == station) 
                ingredients.Add(gameObject);

        foreach (GameObject go in _ingredientList)
        {
            Food food = go.GetComponent<Food>();
            food.GetFoodByStation(ingredients, station);
        }
    }
}
