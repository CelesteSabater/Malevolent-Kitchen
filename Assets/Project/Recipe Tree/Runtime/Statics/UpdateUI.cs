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

            HeatStation heatStation = station as HeatStation;
            if (heatStation == null)
                return;     

            TimerController timer = heatStation.GetTimerController();
            
            if (timer == null) 
                return;
    
            timer.gameObject.SetActive(false);

            if (heatStation.GetFoodIsBurnt())
                return;

            if (station.GetCurrentRecipe() != null)
                timer.gameObject.SetActive(true);
            
            timer.SetBurnt(heatStation.GetFoodIsReady());
        }

        private static void UpdateExclamationIcon(CookingStation station)
        {
            if (station == null)
                return;

            HeatStation heatStation = station as HeatStation;
            if (heatStation == null)
                return; 

            GameObject exclamationIcon = heatStation.GetExclamationIcon();
            if (exclamationIcon == null)
                return;

            if (heatStation.GetStationIsOn() && station.GetCurrentRecipe() == null)
                exclamationIcon.gameObject.SetActive(true);
            else if (heatStation.GetFoodIsBurnt())
                exclamationIcon.gameObject.SetActive(true);
            else
                exclamationIcon.gameObject.SetActive(false);
        }
    }
}