using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Project.RecipeTree.Runtime
{
    public class FryingNode : RecipeNode
    {
        [HideInInspector] public List<RecipeNode> _children = new List<RecipeNode>();
        [SerializeField] private int _numberOfFlips = 2;
        [SerializeField] private float _readyTime = 30;
        [SerializeField] private float _burntTime = 15;

        public int GetNumberOfFlips() => _numberOfFlips;
        public float GetReadyTime() => _readyTime;
        public float GetBurntTime() => _burntTime;

        public override RecipeNode Clone()
        {
            FryingNode node = Instantiate(this);
            node._children = _children.ConvertAll(child => child.Clone());  
            return node;
        }
    }
}