using UnityEngine;

public class GarbageBin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.DestroyFood(other.transform.gameObject, true);
    }
}