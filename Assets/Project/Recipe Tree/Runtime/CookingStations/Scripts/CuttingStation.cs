using Project.RecipeTree.Runtime;
using UnityEngine;

public class CuttingStation : CookingStation
{
    private int _currentCuts = 0;
    private int _maxCuts = 0;
    private GameObject _cuttable;

    #region Getters and Setters
    public int CurrentCuts() => _currentCuts;
    public int GetMaxNumberOfCuts() => _maxCuts;
    #endregion

    protected override void Start()
    {
        base.Start();
        GameEvents.current.onCut += OnCut;
    }

    protected override void OnDestroy() 
    {
        base.OnDestroy();
        GameEvents.current.onCut -= OnCut;
    }

    protected override void CheckIngredients()
    {
        base.CheckIngredients();
        
        CuttingNode cuttingNode = GetCurrentRecipe() as CuttingNode;
        if (cuttingNode != null)
            _maxCuts = cuttingNode.GetNumberOfCuts();
    }

    public bool EnoughCuts() => _maxCuts <= _currentCuts;
    private bool ExtraCuts() => _maxCuts < _currentCuts;

    private void OnCut(string stationGuid)
    {
        if (stationGuid != GetStationGuid())
            return;

        _currentCuts++;
        if (ExtraCuts())
            GameEvents.current.ExtraCuts();

        AudioSystem.Instance.PlaySFX("Cut", GetFoodSpawnPos());
    }

    public void SpawnCuttable()
    {
        CuttingNode recipeNode = GetCurrentRecipe() as CuttingNode;
        if (recipeNode == null)
            return;

        Rigidbody rb = _cuttable.GetComponent<Rigidbody>();
        if (rb != null)
            rb.isKinematic = true;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (GetCurrentRecipe() != null)
            return;

        base.OnTriggerEnter(other);

        if (GetCurrentRecipe() == null)
            return;

        if (!other.GetComponent<Food>())
            return;

        _cuttable = Instantiate(other.gameObject, GetFoodSpawnPos(), Quaternion.identity);
    }

    protected override void RestartData()
    {
        base.RestartData();
        _currentCuts = 0;
        _maxCuts = 0;

        bool DONT_SPAWN_INGREDIENTS = false;
        if (_cuttable != null)
            FoodSpawnManager.Instance.DestroyFood(_cuttable, DONT_SPAWN_INGREDIENTS);
        _cuttable = null;

        UpdateVisuals.UpdateCookingStation(this);
    }
}