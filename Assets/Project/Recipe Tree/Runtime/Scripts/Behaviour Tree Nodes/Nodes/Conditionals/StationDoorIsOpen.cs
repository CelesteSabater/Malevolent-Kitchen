using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class StationDoorIsOpen : ConditionalNode
    {
        public bool wantTheDoorToBeOpen = true;
        protected override void OnStart() { }

        protected override bool Question()
        {
            FurnaceStation cookingStation = BlackboardFunctions.GetCookingStation(_blackboard) as FurnaceStation;
            if (cookingStation == null)
                return false; 
            
            bool result;
            if (wantTheDoorToBeOpen)
                result = cookingStation.GetDoorIsOpen();
            else
                result = !cookingStation.GetDoorIsOpen();

            return result;
        }
    }
}