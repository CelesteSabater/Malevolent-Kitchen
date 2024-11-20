using UnityEngine;
using Project.RecipeTree.Runtime;

namespace Project.BehaviourTree.Runtime
{
    public class DoNothing : ActionNode
    {
        

        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate()
        { 
            UpdateVisuals.UpdateCookingStation(BlackboardFunctions.GetCookingStation(_blackboard));
            _blackboard._startedTimer = false; 
            return State.Success;
        }
    }
}
