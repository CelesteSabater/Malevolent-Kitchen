using UnityEngine;
using Project.RecipeTree.Runtime;

public class GarbageBin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FoodSpawnManager.Instance.DestroyFood(other.transform.gameObject, true);
    }
}