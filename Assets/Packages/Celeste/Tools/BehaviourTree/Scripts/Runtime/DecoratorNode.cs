using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

namespace Celeste.Tools.BehaviourTree
{
    public abstract class DecoratorNode : Node
    {
        [HideInInspector] public Node _child;

        public override Node Clone()
        {
            DecoratorNode node = Instantiate(this);
            node._child = _child.Clone();
            return node;
        }
    }
}