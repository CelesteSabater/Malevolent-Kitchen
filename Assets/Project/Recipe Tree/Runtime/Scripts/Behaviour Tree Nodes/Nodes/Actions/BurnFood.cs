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
            CookingStation cookingStation = BlackboardFunctions.GetCookingStation(_blackboard);

            cookingStation.SetFoodIsReady(true);
            cookingStation.SetFoodIsBurnt(true);
             
            return State.Success;
        }
    }
}
