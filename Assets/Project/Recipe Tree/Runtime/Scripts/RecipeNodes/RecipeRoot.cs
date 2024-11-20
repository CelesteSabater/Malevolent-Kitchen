using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Project.RecipeTree.Runtime
{
    public class RecipeRoot : RecipeNode
    {
        [HideInInspector] public List<RecipeNode> _children = new List<RecipeNode>();
        [SerializeField] private Sprite[] _recipeImages;
        public Sprite[] GetRecipe() => _recipeImages;
        
        public override RecipeNode Clone()
        {
            RecipeRoot node = Instantiate(this);
            node._children = _children.ConvertAll(child => child.Clone());  
            return node;
        }

    }
}
