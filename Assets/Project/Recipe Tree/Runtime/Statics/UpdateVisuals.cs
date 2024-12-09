using UnityEngine;

namespace Project.RecipeTree.Runtime
{
    public static class UpdateVisuals
    {
        public static void UpdateCookingStation(CookingStation station)
        {
            if (station == null)
                return;
                
            UpdateTools(station);
            UpdateParticleSystem(station);
            UpdateLights(station);
            UpdateUI.UpdateStationUI(station);
        }

        private static void UpdateTools(CookingStation station)
        {
            if (station == null)
                return;
                
            HeatStation heatStation = station as HeatStation;
            if (heatStation == null)
                return; 

            GameObject tool = heatStation.GetTool();
            if (tool == null)
                return;

            tool.SetActive(station.GetCurrentRecipe() != null);
        }

        private static void UpdateLights(CookingStation station)
        {
            if (station == null)
                return;

            HeatStation heat = station as HeatStation;
            if (heat == null)
                return;	

            Light l = heat.GetLight();
            if (l == null)
                return;

            l.gameObject.SetActive(heat.GetStationIsOn());
        }                    

        private static void UpdateParticleSystem(CookingStation station)
        {
            if (station == null)
                return;

            HeatStation heatStation = station as HeatStation;
            if (heatStation == null)
                return; 

            ParticleSystem particleSystem = heatStation.GetParticleSystem();
            ParticleSystemData particleSystemData = heatStation.GetParticleSystemData();

            if (particleSystem == null || particleSystemData == null)
                return;
            
            FoodState foodState;

            if (heatStation.GetFoodIsBurnt())
                foodState = FoodState.Burnt;
            else if (heatStation.GetFoodIsReady())
                foodState = FoodState.Ready;
            else if (heatStation.GetCurrentRecipe() != null)
                foodState = FoodState.Cooking;
            else
                foodState = FoodState.Raw;

            particleSystemData.UpdateParticles(station, foodState);
        }
    }
}