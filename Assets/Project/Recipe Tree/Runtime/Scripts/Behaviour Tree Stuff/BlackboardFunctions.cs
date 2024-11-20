using Project.BehaviourTree.Runtime;
using Project.RecipeTree.Runtime;
using UnityEngine;

public static class BlackboardFunctions
{
    public static void ResetTimers(Blackboard blackboard)
    {
        if (blackboard._startedTimer)
            return; 
            
        blackboard._startedTimer = true;
        blackboard._timer1 = 0;
        blackboard._timer2 = 0;
        blackboard._timerDuration1 = 0;
        blackboard._timerDuration2 = 0;

        CookingStation cookingStation = GetCookingStation(blackboard);

        if (cookingStation == null)
        {
            blackboard._timerDuration1 = CookingManager.Instance.GetRecipeTime();
            blackboard._timer1 = blackboard._timerDuration1;
            return;
        }

        RecipeNode recipeNode = cookingStation.GetCurrentRecipe();

        switch(recipeNode)
        {
            case FryingNode _node:
                blackboard._timerDuration1 = _node.GetReadyTime();
                blackboard._timerDuration2 = _node.GetBurntTime();
                break;
            case FurnaceNode _node:
                blackboard._timerDuration1 = _node.GetReadyTime();
                blackboard._timerDuration2 = _node.GetBurntTime();
                break;
            case PotNode _node:
                blackboard._timerDuration1 = _node.GetReadyTime();
                blackboard._timerDuration2 = _node.GetBurntTime();
                break;
            case CuttingNode _node:
                break;
            case MixingNode _node:
                break;
        }
        blackboard._timer1 = blackboard._timerDuration1;
        blackboard._timer2 = blackboard._timerDuration2;
    }

    public static CookingStation GetCookingStation(Blackboard blackboard)
    {
        return blackboard._gameObject.GetComponent<CookingStation>();
    }
}