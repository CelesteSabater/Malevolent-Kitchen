using UnityEngine;

namespace Project.RecipeTree.Runtime
{
    public static class UpdateUI
    {
        public static void UpdateStationUI(CookingStation station)
        {
            if (station == null)
                return;

            UpdateTimer(station);
            UpdateExclamationIcon(station);
        }

        private static void UpdateTimer(CookingStation station)
        {
            if (station == null)
                return;

            TimerController timer = station.GetTimerController();
            
            if (timer == null) 
                return;
    
            timer.gameObject.SetActive(false);

            if (station.GetFoodIsBurnt())
                return;

            if (station.GetCurrentRecipe() != null)
                timer.gameObject.SetActive(true);
            
            timer.SetBurnt(station.GetFoodIsReady());
        }

        private static void UpdateExclamationIcon(CookingStation station)
        {
            if (station == null)
                return;
            GameObject exclamationIcon = station.GetExclamationIcon();
            
            if (exclamationIcon == null)
                return;

            

            if (station.GetStationIsOn() && station.GetCurrentRecipe() == null)
                exclamationIcon.gameObject.SetActive(true);
            else if (station.GetFoodIsBurnt())
                exclamationIcon.gameObject.SetActive(true);
            else
                exclamationIcon.gameObject.SetActive(false);

            Animator anim = exclamationIcon.GetComponentInChildren<Animator>();
            if (anim == null)
                return;
        }
    }
}