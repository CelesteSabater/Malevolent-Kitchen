using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onStrike;
    public void Strike()
    {
        if(onStrike != null)
            onStrike();
    }

    public event Action onCompleteFood;
    public void CompleteFood()
    {
        if(onCompleteFood != null)
            onCompleteFood();
    }

    public event Action<string> onRecipeStep;
    public void RecipeStep(string stationGuid)
    {
        if(onRecipeStep != null)
            onRecipeStep(stationGuid);
    }

    public event Action<string> onBurnFood; 
    public void BurnFood(string stationGuid)
    {
        if(onBurnFood != null)
            onBurnFood(stationGuid);
    }

    public event Action<string> onNewRecipe;
    public void NewRecipe(string recipeName)
    {
        if(onNewRecipe != null)
            onNewRecipe(recipeName);
    }

    public event Action<float> onAnnoy;
    public void Annoy(float pct)
    {
        if(onAnnoy != null)
            onAnnoy(pct);
    }

    public event Action<string> onCut;
    public void Cut(string stationGuid)
    {   
        if(onCut != null)
            onCut(stationGuid);
    }

    public event Action<string> onFlip;
    public void Flip(string stationGuid)
    {   
        if(onFlip != null)
            onFlip(stationGuid);
    }

    public event Action onExtraCuts;
    public void ExtraCuts()
    {   
        if(onExtraCuts != null)
            onExtraCuts();
    }

    public event Action<string> onStartStation; 
    public void StartStation(string stationGuid)
    {
        if(onBurnFood != null)
            onStartStation(stationGuid);
    }

    public event Action<string> onOperateDoor; 
    public void OperateDoor(string stationGuid)
    {
        if(onOperateDoor != null)
            onOperateDoor(stationGuid);
    }
}