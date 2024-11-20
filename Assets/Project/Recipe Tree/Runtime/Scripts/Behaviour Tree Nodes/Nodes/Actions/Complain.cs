using Project.RecipeTree.Runtime;
using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class Complain : ActionNode
    {
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate()
        {
            UpdateVisuals.UpdateCookingStation(BlackboardFunctions.GetCookingStation(_blackboard));
            return State.Success;
        }
    }
}
