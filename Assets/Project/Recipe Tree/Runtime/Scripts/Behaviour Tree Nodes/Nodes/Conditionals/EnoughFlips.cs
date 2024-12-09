using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class EnoughFlips : ConditionalNode
    {
        protected override void OnStart() { }

        protected override bool Question()
        {
            FryingStation cookingStation = BlackboardFunctions.GetCookingStation(_blackboard) as FryingStation;
            if (cookingStation == null)
                return false; 

            return cookingStation.EnoughFlips();
        }
    }
}