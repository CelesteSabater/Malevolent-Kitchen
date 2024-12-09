using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Project.RecipeTree.Runtime
{
    [CreateAssetMenu(fileName = "State Data", menuName = "Tree/RecipeTree/ParticleSystem/ParticleSystem")]
    public class ParticleSystemData : ScriptableObject
    {
        [SerializedDictionary("Food State", "Particle System")]
        public SerializedDictionary<FoodState, ParticleSystemStateData> particleSystems = new SerializedDictionary<FoodState, ParticleSystemStateData>();

        public void UpdateParticles(CookingStation station, FoodState state)
        {
            if (station == null)
                return;

            HeatStation heatStation = station as HeatStation;
            if (heatStation == null)
                return;     

            ParticleSystem particleSystem = heatStation.GetParticleSystem();

            if (particleSystem == null)
                return;

            if (state == FoodState.Raw)
            {
                particleSystem.Stop();
                return;
            }

            ParticleSystemStateData currentState = particleSystems[state];
            SetParticles(particleSystem, currentState);
        }

        private static void SetParticles(ParticleSystem particleSystem, ParticleSystemStateData currentState)
        {
            var color = particleSystem.main;
            color.startColor = currentState.GetStartColor();

            var colorLifeTime = particleSystem.colorOverLifetime;
            colorLifeTime.color = currentState.GetColorOverLifetime();
            
            var rate = particleSystem.emission;
            rate.rateOverTime = currentState.GetRateOverTime();

            particleSystem.Play();
        }
    }
}