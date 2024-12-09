using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class FoodReadyOrBurnt : ConditionalNode
    {
        

        protected override void OnStart() { }

        protected override bool Question()
        {
            
            HeatStation cookingStation = BlackboardFunctions.GetCookingStation(_blackboard) as HeatStation;
            if (cookingStation == null)
                return false; 
                
            return cookingStation.GetFoodIsReady() || cookingStation.GetFoodIsBurnt();
        }
    }
}