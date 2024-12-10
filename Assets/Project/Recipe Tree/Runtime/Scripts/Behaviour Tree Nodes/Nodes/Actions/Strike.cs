using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class Strike : ActionNode
    {
        protected override void OnStart() { }
        protected override void OnStop() { }

        protected override State OnUpdate()
        { 
            GameEvents.current.Strike();
            
            return State.Success;
        }
    }
}
