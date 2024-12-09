using UnityEngine;
using StarterAssets;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];
    private IInteractable _interactable;
    private StarterAssetsInputs _input;

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        CheckInteractable();
        Interact();
    }

    private void CheckInteractable()
    {   
        if (_interactable != null)
        {
            _interactable.SetupPrompt(false);
        }

        _interactable = null;

        if (Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask) > 0)
        {
            _interactable = _colliders[0].GetComponent<IInteractable>();
            _interactable.SetupPrompt(true);
        }
    }

    private void Interact()
    {
        if (!_input.interact)
            return;

        if (_interactable != null)
        {
            _interactable.Interact(this);
        }

        _input.interact = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
