using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Runtime.Interaction
{
    public class Button : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _promptText;
        [SerializeField] private GameObject _promptPrefab;
        [SerializeField] private Transform _promtLocation;
        [SerializeField] private bool _isDisplay;
        
        [SerializeField] private CookingStation _cookingStation;
        [SerializeField] private Vector3 _nudge;

        public string InteractPrompt => _promptText;
        public GameObject PromptPrefab => _promptPrefab;
        public Transform PromptLocation => _promtLocation;

        public bool IsDisplay => _isDisplay;

        private SpringToScale _springToScale;
        private GameObject _promptGo;

        private void Start()
        {
            _springToScale = GetComponent<SpringToScale>();
            _promptGo = Instantiate(_promptPrefab, PromptLocation.position, Quaternion.identity);
            TextMeshProUGUI textMeshPro = _promptGo.GetComponentInChildren<TextMeshProUGUI>();
            textMeshPro.text = _promptText;

            _promptGo.SetActive(false);
        }       


        public bool Interact(Interactor interactor)
        {
            GameEvents.current.StartStation(_cookingStation.GetStationGuid());
            _springToScale.Nudge(_nudge);

            return true;
        }

        public void SetupPrompt(bool show)
        {
            _promptGo.SetActive(show);
            _isDisplay = show;
        }
    }
}