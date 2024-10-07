using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Cooking Settings")]
    [SerializeField] private List<GameObject> _menu = new List<GameObject>();
    [SerializeField] private int _maxStrikes = 3;
    [SerializeField] private List<GameObject> _foodSpawnPos = new List<GameObject>();

    [Header("Summon Food Settings")]
    [SerializeField] private GameObject _foodSpawnEffect;
    [SerializeField] private Vector3 _spawnDisplace = new Vector3(0, 0.2f, 0);
    [SerializeField] private float _summonDelay = 3;


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

    private void InstanceIngredients(Food food)
    {
        List<GameObject> ingredients = food.GetRawIngredients();

        int i = 0;
        foreach (GameObject go in ingredients)
        {
            SpawnFood(go, _foodSpawnPos[i].transform.position);
            i++;
        }
    }

    public void DestroyFood(GameObject go)
    {
        Food food = go.GetComponent<Food>();
        if (food != null) InstanceIngredients(food);
        DestroyImmediate(go);
    }

    public void DestroyAllFood()
    {
        GameObject[] food = GameObject.FindGameObjectsWithTag("Food");
        foreach (GameObject f in food)
        {
            DestroyFood(f);
        }
    }

    public void SpawnFood(GameObject go, Vector3 pos)
    {
        Instantiate(_foodSpawnEffect, pos, Quaternion.identity);
        StartCoroutine(Spawn(_summonDelay, go, pos));
    }

    IEnumerator Spawn(float delay, GameObject go, Vector3 pos)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(go, pos + _spawnDisplace, Quaternion.identity);
    }
}
