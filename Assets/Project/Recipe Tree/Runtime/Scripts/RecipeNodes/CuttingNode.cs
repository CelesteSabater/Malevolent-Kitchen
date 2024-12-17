using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.RecipeTree.Runtime
{
    public class CuttingNode : RecipeNode
    {
        [HideInInspector] public RecipeNode _child;
        [SerializeField] private int _numberOfCuts = 2;

        public int GetNumberOfCuts() => _numberOfCuts;

        public override RecipeNode Clone()
        {
            CuttingNode node = Instantiate(this);
            node._child = _child.Clone();
            return node;
        }
    }
}