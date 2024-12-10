using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class GameOver : ActionNode
    {
        protected override void OnStart() { }
        protected override void OnStop() { }

        protected override State OnUpdate()
        { 
            CookingManager.Instance.GameOver();
             
            return State.Success;
        }
    }
}
