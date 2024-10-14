using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Nodes/SequencerNode")]
public class SequencerNode : CompositeNode
{
    private int _current;
    
    protected override void OnStart()
    {
        _current = 0;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        Node child = _children[_current];
        switch (child.Update())
        {
            case State.Running:
                return State.Running;
            case State.Failure:
                return State.Failure;
            case State.Success:
                _current++;
                break;
        }

        return _current == _children.Count ? State.Success : State.Running;
    }
}
