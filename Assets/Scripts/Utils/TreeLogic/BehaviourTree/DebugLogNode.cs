using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BehaviourTree/Nodes/DebugLogNode")]
public class DebugLogNode : ActionNode
{
    [SerializeField] private string message;
    protected override void OnStart() { }

    protected override void OnStop() { }

    protected override State OnUpdate()
    {
        Debug.Log($"Debug: {message}");
        return State.Success;
    }
}
