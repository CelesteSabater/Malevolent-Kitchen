using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CookingStation : MonoBehaviour
{
    [SerializeField] private Station _stationType;
    [SerializeField] private GameObject _foodSpawnPos;
    private GameObject _currentRecipe;
    private List<GameObject> _stationRecipes = new List<GameObject>();  
    private List<GameObject> _ingredientsOnArea = new List<GameObject>();

    private void Start()
    {
        _stationRecipes = CookingController.Instance.GetRecipes(_stationType);
    }

    private void CheckIngredients()
    {
        _currentRecipe = null;
        _ingredientsOnArea = _ingredientsOnArea.OrderBy(go => go.name).ToList();

        foreach (GameObject recipe in _stationRecipes)
        {
            
        }
    }

    private void CompleteCookingChallenge()
    {
        _currentRecipe = null;
        GameController.Instance.SpawnFood(_currentRecipe, _foodSpawnPos.transform.position);
    }

    private void FailCookingChallenge()
    {
        _currentRecipe = null;
        GameController.Instance.SpawnFood(_currentRecipe.GetComponent<Food>().GetFailedFood(), _foodSpawnPos.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Food>()) _ingredientsOnArea.Add(other.gameObject);
        CheckIngredients();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Food>()) _ingredientsOnArea.Remove(other.gameObject);
        CheckIngredients();
    }
}
