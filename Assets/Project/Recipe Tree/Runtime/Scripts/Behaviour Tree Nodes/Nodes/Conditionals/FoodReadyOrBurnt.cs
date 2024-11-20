using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class FoodReadyOrBurnt : ConditionalNode
    {
        

        protected override void OnStart() { }

        protected override bool Question()
        {
            
            CookingStation cookingStation = BlackboardFunctions.GetCookingStation(_blackboard);
            return cookingStation.GetFoodIsReady() || cookingStation.GetFoodIsBurnt();
        }
    }
}