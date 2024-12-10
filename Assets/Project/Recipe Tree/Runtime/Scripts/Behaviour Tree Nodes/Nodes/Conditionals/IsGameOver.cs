using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class IsGameOver : ConditionalNode
    {
        protected override void OnStart() { }
        
        protected override bool Question() => CookingManager.Instance.GetGameIsLost();
    }
}