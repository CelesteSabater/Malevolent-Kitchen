using UnityEngine;

public class MixingStation : CookingStation
{

    protected override void CheckIngredients()
    {
        base.CheckIngredients();
        if(GetCurrentRecipe() != null)
            OnRecipeStep(GetStationGuid());
    }

}