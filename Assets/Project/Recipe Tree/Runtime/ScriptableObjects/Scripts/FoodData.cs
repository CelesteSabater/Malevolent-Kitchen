using System;
using UnityEditor;
using UnityEngine;

namespace Project.RecipeTree.Runtime
{
    [CreateAssetMenu(fileName = "Food Data", menuName = "Tree/RecipeTree/FoodData")]
    public class FoodData: ScriptableObject
    {
        [SerializeField] private string _foodName;
        [SerializeField] private GameObject _prefab;

        public string GetFoodName() => _foodName;
        public GameObject GetPrefab() => _prefab;
    }
}