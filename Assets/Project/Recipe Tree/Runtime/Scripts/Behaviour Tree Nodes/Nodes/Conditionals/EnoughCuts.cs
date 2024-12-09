using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class EnoughCuts : ConditionalNode
    {
        protected override void OnStart() { }

        protected override bool Question()
        {
            CuttingStation cookingStation = BlackboardFunctions.GetCookingStation(_blackboard) as CuttingStation;
            if (cookingStation == null)
                return false; 

            return cookingStation.EnoughCuts();
        }
    }
}