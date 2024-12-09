using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class SpawnCuttable : ActionNode
    {
        

        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate()
        { 
            
            CuttingStation cookingStation = BlackboardFunctions.GetCookingStation(_blackboard) as CuttingStation;
            if (cookingStation == null)
	            return State.Failure; 
            
            cookingStation.SpawnCuttable();
            
            return State.Success;
        }
    }
}
