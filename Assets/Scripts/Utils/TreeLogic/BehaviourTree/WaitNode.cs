using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Nodes/WaitNode")]
public class WaitNode : ActionNode
{
    [SerializeField] private float _duration = 1;
    private float _startTime;

    protected override void OnStart()
    {
        _startTime = Time.time;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (Time.time - _startTime > _duration) return State.Success; 
        return State.Running;
    }
}
