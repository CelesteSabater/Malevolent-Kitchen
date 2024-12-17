using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.RecipeTree.Runtime
{
    public class MixingNode : RecipeNode
    {
        public List<RecipeNode> _children = new List<RecipeNode>();

        public override RecipeNode Clone()
        {
            MixingNode node = Instantiate(this);
            node._children = _children.ConvertAll(child => child.Clone());  
            return node;
        }
    }
}