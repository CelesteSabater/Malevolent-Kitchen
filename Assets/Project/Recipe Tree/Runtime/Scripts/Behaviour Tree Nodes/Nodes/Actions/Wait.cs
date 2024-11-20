using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class Wait : ActionNode
    {
        public bool _timer1Active, _timer2Active;
        

        protected override void OnStart() 
        {
            
            BlackboardFunctions.ResetTimers(_blackboard);
        }

        protected override void OnStop() { }

        protected override State OnUpdate()
        {
            
            CookingStation cookingStation = BlackboardFunctions.GetCookingStation(_blackboard);

            if (_timer1Active)
            {
                _blackboard._timer1 = _blackboard._timer1 - Time.deltaTime;
                if (_blackboard._timer1 <= 0)
                    _blackboard._timer1 = 0;
                
                if (cookingStation != null)
                    cookingStation.UpdateTimer(_blackboard._timer1 / _blackboard._timerDuration1);
                    
            }

            if (_timer2Active)
            {
                _blackboard._timer2 = _blackboard._timer2 - Time.deltaTime;
                if (_blackboard._timer2 <= 0)
                    _blackboard._timer2 = 0;  
                
                if (cookingStation != null)
                    cookingStation.UpdateTimer(_blackboard._timer2 / _blackboard._timerDuration2);
            }            

            return State.Success;
        }
    }
}