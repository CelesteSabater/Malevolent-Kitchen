using UnityEngine;

namespace Project.RecipeTree.Runtime
{
    [CreateAssetMenu(fileName = "State Data", menuName = "Tree/RecipeTree/ParticleSystem/StateData")]
    public class ParticleSystemStateData : ScriptableObject
    {
        [SerializeField] private Color _startColor;
        [SerializeField] private Gradient _colorOverLifetime;
        [SerializeField] private float _rateOverTime;

        public Color GetStartColor() => _startColor; 
        public void SetStartColor(Color value) => _startColor = value;

        public Gradient GetColorOverLifetime() => _colorOverLifetime;
        public void SetColorOverLifetime(Gradient value) => _colorOverLifetime = value;

        public float GetRateOverTime() => _rateOverTime;
        public void SetRateOverTime(float value) => _rateOverTime = value;
  
    }
}