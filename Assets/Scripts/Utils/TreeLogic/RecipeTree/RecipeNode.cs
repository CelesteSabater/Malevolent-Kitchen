using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeNode : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private List<RecipeNode> _childrenNodes = new List<RecipeNode>();

    [SerializeField] private GameObject _prefab, _failedCooking;
    [SerializeField] private Station _station;
    //[SerializeField] private ActionTree _action;

    public string GetName() => _name;
    public List<RecipeNode> GetChildrenNodes => _childrenNodes;
    public GameObject GetPrefab() => _prefab;
    public GameObject GetFailedCooking() => _failedCooking;
    public Station GetStation() => _station;
    //public ActionTree GetAction() => _action;
}
