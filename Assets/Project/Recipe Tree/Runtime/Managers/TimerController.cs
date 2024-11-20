using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.RecipeTree.Runtime
    {
    [Serializable]
    struct UIColor
    {
        public string primaryColor;
        public string secondaryColor;
        public string fillColor;
    }

    public class TimerController : MonoBehaviour
    {
        private Slider _slider;
        [SerializeField] private GameObject _view, _outerBorder, _innerBorder, _fill, _center;
        [SerializeField] private UIColor _normalPalette, _burntPalette;

        //public void Start() => _slider = GetComponentInChildren<Slider>(true);
        public void Start()
        {
            _slider = GetComponentInChildren<Slider>(true);
            SetBurnt(true);
        }
        public void SetRender(bool render) => _view.SetActive(render);
        public void SetFill(float percent) => _slider.value = percent;

        public void SetBurnt(bool burnt)
        {
            Color primaryCol, secondaryCol, fillCol;
            if (burnt)
            {
                ColorUtility.TryParseHtmlString(_burntPalette.primaryColor, out primaryCol);
                ColorUtility.TryParseHtmlString(_burntPalette.secondaryColor, out secondaryCol);
                ColorUtility.TryParseHtmlString(_burntPalette.fillColor, out fillCol);
            }
            else 
            {
                ColorUtility.TryParseHtmlString(_normalPalette.primaryColor, out primaryCol);
                ColorUtility.TryParseHtmlString(_normalPalette.secondaryColor, out secondaryCol);
                ColorUtility.TryParseHtmlString(_normalPalette.fillColor, out fillCol);
            }

            _outerBorder.GetComponent<UnityEngine.UI.Image>().color = primaryCol;
            _innerBorder.GetComponent<UnityEngine.UI.Image>().color = primaryCol;
            _center.GetComponent<UnityEngine.UI.Image>().color = secondaryCol;
            _fill.GetComponent<UnityEngine.UI.Image>().color = fillCol;
        }
    }
}