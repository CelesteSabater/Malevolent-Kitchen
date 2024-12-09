using UnityEngine;

public class FryingStation : HeatStation
{
    private int _flipsRequired = 0;
    private int _flipsPerformed = 0;

    protected override void Start()
    {
        base.Start();
        GameEvents.current.onFlip += OnFlip;
    }

    protected override void OnDestroy() 
    {
        base.OnDestroy();
        GameEvents.current.onFlip -= OnFlip;
    }

    public bool EnoughFlips()
    {
        throw new System.NotImplementedException();
    }

    public void SpawnFlippable()
    {
        throw new System.NotImplementedException();
    }

    private void OnFlip(string stationGuid)
    {
        if (stationGuid != GetStationGuid())
            return;

        _flipsPerformed++;
        AudioSystem.Instance.PlaySFX("Flip", GetFoodSpawnPos());
    }

}