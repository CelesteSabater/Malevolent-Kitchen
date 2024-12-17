using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.RecipeTree.Runtime
{
    public class FurnaceNode : RecipeNode
    {
        [HideInInspector] public List<RecipeNode> _children = new List<RecipeNode>();
        [SerializeField] private float _readyTime = 10;
        [SerializeField] private float _burntTime = 5;

        public float GetReadyTime() => _readyTime;
        public float GetBurntTime() => _burntTime;

        public override RecipeNode Clone()
        {
            FurnaceNode node = Instantiate(this);
            node._children = _children.ConvertAll(child => child.Clone());  
            return node;
        }
    }
}