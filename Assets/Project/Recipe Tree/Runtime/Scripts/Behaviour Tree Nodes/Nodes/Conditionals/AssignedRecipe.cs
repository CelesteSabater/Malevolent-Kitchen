using Project.RecipeTree.Runtime;
using UnityEngine;

namespace Project.BehaviourTree.Runtime
{
    public class AssignedRecipe : ConditionalNode
    {

        protected override void OnStart() { }

        protected override bool Question()
        {
            
            bool b = true;
            
            CookingStation station = BlackboardFunctions.GetCookingStation(_blackboard);
            RecipeNode recipe = station.GetCurrentRecipe();
            if(recipe == null)
                b = false;

            return b;
        }
    }
}