using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class RestartData : ActionNode
    {
        protected override void OnStart() { }
        protected override void OnStop() { }

        protected override State OnUpdate()
        { 
            CookingManager.Instance.RestartData();
             
            return State.Success;
        }
    }
}
