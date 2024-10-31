using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Celeste.Tools.RecipeTree;
using UnityEngine;

public class Food: MonoBehaviour
{
    [SerializeField] private string _name;
    private List<FoodData> _children = new List<FoodData>();

    public void SetName(string name) => _name = name;
    public string GetName() => _name;
    public void AddChildren(FoodData f) => _children.Add(f);
    public void RemoveChildren(FoodData f) => _children.Remove(f);
    public void SetChildren(List<FoodData> f) => _children = f;
    public List<FoodData> GetChildren() => _children;
}
