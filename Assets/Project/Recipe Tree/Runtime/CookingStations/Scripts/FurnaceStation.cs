using UnityEngine;
using Project.RecipeTree.Runtime;

public class FurnaceStation : HeatStation
{
    [Header("Furnace Settings")]
    [SerializeField] private GameObject _door;
    private bool _doorIsOpen = false;

    #region Getters and Setters
    public bool GetDoorIsOpen() => _doorIsOpen;
    #endregion

    protected override void Start()
    {
        base.Start();
        GameEvents.current.onOperateDoor += OnOperateDoor;
    }

    protected override void OnDestroy() 
    {
        base.OnDestroy();
        GameEvents.current.onOperateDoor -= OnOperateDoor;
    }

    protected override void OnRecipeStep(string stationGuid)
    {
        if (stationGuid != GetStationGuid())
            return;
        
        RecipeNode node = GetCurrentRecipe();
        if (node == null || !GetFoodIsReady())
            return;     

        RecipeRoot root = node as RecipeRoot;
        if (root != null)
            AudioSystem.Instance.PlayMusicWithDelay(FoodSpawnManager.Instance.GetSummonDelay(),"WeDidIt");  

        FoodSpawnManager.Instance.InstanceFoodWithoutBeam(node.GetFoodData(), GetFoodSpawnPos(), GetFoodIsBurnt());
        RestartData();   
    }

    private void OnOperateDoor(string stationGuid)
    {
        if (stationGuid != GetStationGuid())
            return;

        _doorIsOpen = !_doorIsOpen;
        if (_doorIsOpen)
        {
            AnimationSystem.Instance.PlayAnimation(_door.GetComponent<Animator>(),"Open");
            AudioSystem.Instance.PlaySFX("OpenDoor",_door.transform.position);
        }
        else
        {
            AnimationSystem.Instance.PlayAnimation(_door.GetComponent<Animator>(),"Close");
            AudioSystem.Instance.PlaySFXWithDelay(0.5f, "CloseDoor",_door.transform.position);
        }
    }
}