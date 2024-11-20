using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class StationIsOn : ConditionalNode
    {
        

        protected override void OnStart() { }

        protected override bool Question()
        {
            CookingStation station = BlackboardFunctions.GetCookingStation(_blackboard);
            return station.GetStationIsOn();
        }
    }
}