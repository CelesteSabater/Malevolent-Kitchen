using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class RestartTimers : ActionNode
    {
        protected override void OnStart() 
        {
            BlackboardFunctions.ResetTimers(_blackboard);
        }

        protected override void OnStop() { }

        protected override State OnUpdate()
        { 
            _blackboard._timer1 = 0;
            _blackboard._timerDuration1 = 0;
            _blackboard._timer2 = 0;
            _blackboard._timerDuration2 = 0;
             
            return State.Success;
        }
    }
}
