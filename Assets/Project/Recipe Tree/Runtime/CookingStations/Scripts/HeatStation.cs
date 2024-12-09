using UnityEngine;
using Project.RecipeTree.Runtime;
public class HeatStation : CookingStation
{
    [Header("Visual Effects")]
    [SerializeField] private GameObject _exclamationIcon;
    [SerializeField] private GameObject _tool;
    [SerializeField] private TimerController _timer;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Light _light;

    [Header("Particle Settings")]
    [SerializeField] private ParticleSystemData _particleSystemData;

    private bool _stationIsOn = false;
    private bool _foodIsBurnt = false;
    private bool _foodIsReady = false;

    #region Getters and Setters
    public bool GetStationIsOn() => _stationIsOn;
    public bool GetFoodIsBurnt() => _foodIsBurnt;
    public void SetFoodIsBurnt(bool value)
    {
        if (value == _foodIsBurnt)
            return;

        _foodIsBurnt = value;

        if (_foodIsBurnt)
            AudioSystem.Instance.PlaySFX("Burnt",gameObject.transform.position);
    }
    public bool GetFoodIsReady() => _foodIsReady;
    public void SetFoodIsReady(bool value)
    {
        if (value == _foodIsReady)
            return;

        _foodIsReady = value;

        if (_foodIsReady)
            AudioSystem.Instance.PlaySFX("Ding",gameObject.transform.position);
    }
    public GameObject GetTool() => _tool;
    public GameObject GetExclamationIcon() => _exclamationIcon;
    public TimerController GetTimerController() => _timer;
    public ParticleSystem GetParticleSystem() => _particleSystem;
    public ParticleSystemData GetParticleSystemData() => _particleSystemData;
    public Light GetLight() => _light;
    #endregion

    protected override void Start()
    {
        base.Start();
        GameEvents.current.onBurnFood += OnBurnFood;
        GameEvents.current.onStartStation += OnStartStation;
    }

    protected override void OnDestroy() 
    {
        base.OnDestroy();
        GameEvents.current.onBurnFood -= OnBurnFood;
        GameEvents.current.onStartStation -= OnStartStation;
    }

    private void OnBurnFood(string stationGuid)
    {
        if (stationGuid != GetStationGuid())
            return;
        
        RecipeNode node = GetCurrentRecipe();
        if (node == null || !_foodIsBurnt)
            return;  
        
        FoodSpawnManager.Instance.InstanceFood(node.GetFoodData(), GetFoodSpawnPos(), _foodIsBurnt);
        RestartData();
    }

    public void OnStartStation(string stationGuid)
    {
        if (stationGuid != GetStationGuid())
            return;
            
        _stationIsOn = !_stationIsOn;

        AudioSystem.Instance.PlaySFX("Click",gameObject.transform.position);
    }

    public void UpdateTimer(float percent) 
    {
        UpdateVisuals.UpdateCookingStation(this);
        _timer.SetFill(percent);
    }

    protected override void RestartData()
    {
        base.RestartData();
        _stationIsOn = false;
        _foodIsBurnt = false;
        _foodIsReady = false;
        UpdateVisuals.UpdateCookingStation(this);
    }
}