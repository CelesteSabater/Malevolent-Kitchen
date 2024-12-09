using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class HoldingKnife : ConditionalNode
    {
        public bool test;
        protected override bool Question() => test;
    }
}