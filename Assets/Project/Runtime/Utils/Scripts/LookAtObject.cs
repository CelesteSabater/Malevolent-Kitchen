using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] private LookAtObjectType billboardType;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject gameObject;
    [Header("Lock Rotation")]
    [SerializeField] private bool lockX;
    [SerializeField] private bool lockY;
    [SerializeField] private bool lockZ;

    private Vector3 originalRotation;

    public enum LookAtObjectType { LookAtObject, CameraForward };

    private void Awake() {
        originalRotation = transform.rotation.eulerAngles;
    }

    // Use Late update so everything should have finished moving.
    void LateUpdate() {
        // There are two ways people billboard things.
        switch (billboardType) {
        case LookAtObjectType.LookAtObject:
            transform.LookAt(gameObject.transform.position, Vector3.up);
            transform.rotation *= Quaternion.Euler(offset);
            break;
        case LookAtObjectType.CameraForward:
            transform.forward = gameObject.transform.forward;
            transform.rotation *= Quaternion.Euler(offset);
            break;
        default:
            break;
        }
        
        Vector3 rotation = transform.rotation.eulerAngles;
        if (lockX) { rotation.x = originalRotation.x; }
        if (lockY) { rotation.y = originalRotation.y; }
        if (lockZ) { rotation.z = originalRotation.z; }
        transform.rotation = Quaternion.Euler(rotation);
    }
}