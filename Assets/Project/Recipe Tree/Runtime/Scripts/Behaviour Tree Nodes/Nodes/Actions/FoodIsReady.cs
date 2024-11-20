using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class FoodIsReady : ActionNode
    {
        

        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate()
        { 
            
            CookingStation cookingStation = BlackboardFunctions.GetCookingStation(_blackboard);
            
            cookingStation.SetFoodIsReady(true);
            
            return State.Success;
        }
    }
}
