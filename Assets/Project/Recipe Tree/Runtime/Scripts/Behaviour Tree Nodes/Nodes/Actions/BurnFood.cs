using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class BurnFood : ActionNode
    {
        protected override void OnStart()
        {
            BlackboardFunctions.ResetTimers(_blackboard);
        }

        protected override void OnStop() { }

        protected override State OnUpdate()
        { 
            HeatStation cookingStation = BlackboardFunctions.GetCookingStation(_blackboard) as HeatStation;
            if (cookingStation == null)
                return State.Failure; 

            cookingStation.SetFoodIsReady(true);
            cookingStation.SetFoodIsBurnt(true);
             
            return State.Success;
        }
    }
}
