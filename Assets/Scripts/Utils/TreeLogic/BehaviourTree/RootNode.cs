using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RootNode : Node
{
    [SerializeField] private bool _repeater = true;
    public Node _child;

    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override State OnUpdate()
    {
        _child.Update();

        SetState(State.Running);

        State childState = _child.GetState();
        if (!_repeater)
            if (childState != State.Running) SetState(childState);

        return GetState();
    }

    public override Node Clone()
    {
        RootNode node = Instantiate(this);
        node._child = _child.Clone();
        return node;
    }

}
