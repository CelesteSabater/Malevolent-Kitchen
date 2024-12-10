using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class SetRecipe : ActionNode
    {
        protected override void OnStart() { }
        protected override void OnStop() { }

        protected override State OnUpdate()
        { 
            CookingManager.Instance.InitializeRandomRequest();
             
            return State.Success;
        }
    }
}
