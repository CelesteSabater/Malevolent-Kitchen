using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public abstract class DecoratorNode : Node
{
    public Node _child;

    public override Node Clone()
    {
        DecoratorNode node = Instantiate(this);
        node._child = _child.Clone();
        return node;
    }
}
