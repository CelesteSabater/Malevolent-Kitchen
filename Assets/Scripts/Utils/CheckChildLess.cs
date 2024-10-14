using UnityEngine;

public class CheckChildLess : MonoBehaviour
{
    void Update()
    {
        if (transform.childCount == 0)
        {
            DestroyImmediate(gameObject);
        }
    }
}
