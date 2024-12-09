using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class TimerIsUp : ConditionalNode
    {
        public bool _timer1Active, _timer2Active;
        
        
        protected override void OnStart()
        {
            BlackboardFunctions.ResetTimers(_blackboard);
        }
        
        protected override bool Question()
        {
            bool answer = false;

            if ((_timer1Active && _blackboard._timer1 <= 0) || (_timer2Active && _blackboard._timer2 <= 0))
                answer = true;

            return answer;
        }
    }
}