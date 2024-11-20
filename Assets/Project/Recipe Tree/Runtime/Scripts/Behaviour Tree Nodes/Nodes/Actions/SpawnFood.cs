using System.Collections;
using System.Collections.Generic;
using Project.RecipeTree.Runtime;
using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class SpawnFood : ActionNode
    {
        

        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate()
        {
            
            CookingStation station = BlackboardFunctions.GetCookingStation(_blackboard);
            RecipeNode recipe = station.GetCurrentRecipe();
            
            if(recipe == null)
                return State.Failure;

            if(!station.GetFoodIsReady())
                return State.Failure;
            
            if (station.GetFoodIsBurnt())
                GameEvents.current.BurnFood(station.GetStationGuid());
            else
                GameEvents.current.RecipeStep(station.GetStationGuid());

            return State.Success;
        }
    }
}
