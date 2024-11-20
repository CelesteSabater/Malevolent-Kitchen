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
            UpdateUI.UpdateStationUI(station);
        }

        private static void UpdateTools(CookingStation station)
        {
            if (station == null)
                return;

            station.GetTool().SetActive(false);
            if (station.GetCurrentRecipe() != null)
                station.GetTool().SetActive(true);
        }

        private static void UpdateParticleSystem(CookingStation station)
        {
            if (station == null)
                return;

            ParticleSystem particleSystem = station.GetParticleSystem();
            ParticleSystemData particleSystemData = station.GetParticleSystemData();

            if (particleSystem == null || particleSystemData == null)
                return;
            
            FoodState foodState;

            if (station.GetFoodIsBurnt())
                foodState = FoodState.Burnt;
            else if (station.GetFoodIsReady())
                foodState = FoodState.Ready;
            else if (station.GetCurrentRecipe() != null)
                foodState = FoodState.Cooking;
            else
                foodState = FoodState.Raw;

            particleSystemData.UpdateParticles(station, foodState);
        }
    }
}