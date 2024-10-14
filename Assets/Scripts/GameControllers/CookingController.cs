using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CookingController : MonoBehaviour
{
    public static CookingController Instance { get; private set; }

    [Header("Cooking Settings")]
    [SerializeField] private List<GameObject> _menu = new List<GameObject>();
    [SerializeField] private int _maxStrikes = 3;
    [SerializeField] private List<GameObject> _foodSpawnPos = new List<GameObject>();

    private int _currentStrikes = 0;
    private Food _currentRequest;
    private Food _lastRequest;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        InitializeRandomRequest();
    }

    public void InitializeRandomRequest()
    {
        do
        {
            _currentRequest = GetRandomRequest().GetComponent<Food>();
        } while (_lastRequest == _currentRequest);
        InstanceIngredients(_currentRequest);
    }

    private GameObject GetRandomRequest()
    {
        int i = Random.Range(0, _menu.Count);
        return _menu[i];
    }

    public void InstanceIngredients(Food food)
    {
        List<GameObject> ingredients = food.GetRawIngredients();

        int i = 0;
        foreach (GameObject go in ingredients)
        {
            GameController.Instance.SpawnFood(go, _foodSpawnPos[i].transform.position);
            i++;
        }
    }

    public List<GameObject> GetRecipes(Station station)
    {
        List<GameObject> recipes = new List<GameObject>();

        foreach (GameObject go in _menu)
        {
            Food food = go.GetComponent<Food>();
            List<GameObject> list = food.GetFoodByStation(station);
            foreach (GameObject go2 in list) recipes.Add(go2);
        }

        return recipes;
    }
}
