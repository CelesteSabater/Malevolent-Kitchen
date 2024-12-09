using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class SpawnFlippable : ActionNode
    {

        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate()
        { 
            
            FryingStation cookingStation = BlackboardFunctions.GetCookingStation(_blackboard) as FryingStation;
            if (cookingStation == null)
	            return State.Failure; 
            
            cookingStation.SpawnFlippable();
            
            return State.Success;
        }
    }
}
