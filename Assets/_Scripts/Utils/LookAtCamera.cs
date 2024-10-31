using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 directionToCamera = cameraPosition - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);
        transform.rotation = lookRotation;
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
    }
}