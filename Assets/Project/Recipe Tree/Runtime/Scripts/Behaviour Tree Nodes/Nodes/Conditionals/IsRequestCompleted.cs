using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class IsRequestCompleted : ConditionalNode
    {
        protected override void OnStart() { }
        
        protected override bool Question() => CookingManager.Instance.GetRequestCompleted();
    }
}