using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public abstract class CompositeNode : Node
    {
         public List<Node> _children = new List<Node>();

        public override Node Clone()
        {
            CompositeNode node = Instantiate(this);
            node._children = _children.ConvertAll(child => child.Clone());  
            return node;
        }
    }
}
