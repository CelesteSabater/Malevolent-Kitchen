using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Celeste.Tools.BehaviourTree
{
    public class Test : ConditionalNode
    {
        public bool test;
        protected override bool Question() => test;
    }
}
