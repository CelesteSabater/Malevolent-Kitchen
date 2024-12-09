using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class FoodIsReady : ActionNode
    {
        

        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate()
        { 
            
            HeatStation cookingStation = BlackboardFunctions.GetCookingStation(_blackboard) as HeatStation;
            if (cookingStation == null)
	            return State.Failure; 
            
            cookingStation.SetFoodIsReady(true);
            
            return State.Success;
        }
    }
}
