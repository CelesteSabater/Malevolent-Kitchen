using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [Header("Summon Food Settings")]
    [SerializeField] private GameObject _foodSpawnEffect;
    [SerializeField] private Vector3 _spawnDisplace = new Vector3(0, 0.2f, 0);
    [SerializeField] private float _summonDelay = 3;
    [SerializeField] private Vector3 _nudgeValue = new Vector3(1f, 1f, 1f);
    [SerializeField] private string _soundSummonName, _soundExplosionName;

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

    public void DestroyFood(GameObject go)
    {
        Food food = go.GetComponent<Food>();
        if (food != null) CookingController.Instance.InstanceIngredients(food);
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
        AudioController.Instance.PlaySFX(_soundSummonName, pos);
        StartCoroutine(Spawn(_summonDelay, go, pos));
    }

    IEnumerator Spawn(float delay, GameObject go, Vector3 pos)
    {
        yield return new WaitForSeconds(delay);
        GameObject go1 = Instantiate(go, pos + _spawnDisplace, Quaternion.identity);
        SpringToScale spring = go1.GetComponent<SpringToScale>();
        AudioController.Instance.PlaySFX(_soundExplosionName, pos);
        if (spring != null) spring.Nudge(_nudgeValue);
    }
}
