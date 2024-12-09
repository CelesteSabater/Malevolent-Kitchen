using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class TimePenalty : ActionNode
    {
        public float _timeMultiplier = 1f;
        

        protected override void OnStart() 
        {
            
            BlackboardFunctions.ResetTimers(_blackboard);
        }

        protected override void OnStop() { }

        protected override State OnUpdate()
        {
            
            HeatStation cookingStation = BlackboardFunctions.GetCookingStation(_blackboard) as HeatStation;
            
            if (_blackboard._timer1 >= 0)
            {
                _blackboard._timer1 = _blackboard._timer1 + Time.deltaTime * _timeMultiplier;
                if (_blackboard._timer1 >= _blackboard._timerDuration1)
                    _blackboard._timer1 = _blackboard._timerDuration1;
                
                if (cookingStation != null)
                    cookingStation.UpdateTimer(_blackboard._timer1 / _blackboard._timerDuration1);
            }

            if (_blackboard._timer2 >= 0)
            {
                _blackboard._timer2 = _blackboard._timer2 + Time.deltaTime * _timeMultiplier;
                if (_blackboard._timer2 >= _blackboard._timerDuration2)
                    _blackboard._timer2 = _blackboard._timerDuration2;  
                
                if (cookingStation != null && _blackboard._timer2 != _blackboard._timerDuration2)
                    cookingStation.UpdateTimer(_blackboard._timer2 / _blackboard._timerDuration2);
            }         

            return State.Success;
        }
    }
}
