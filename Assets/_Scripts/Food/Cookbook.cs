using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Cookbook: MonoBehaviour
{
    [SerializeField] private List<Image> _cookBook = new List<Image>();
    [SerializeField] private Image _leftPage, _rightPage;
    private int _page, _maxPage;

    private void Start()
    {
        _cookBook = CookingManager.Instance.GetCookbook();
        float value = _cookBook.Count / 2;
        _maxPage = (int)value;
    }

    private void DisplayPage()
    {
        _leftPage.sprite = _cookBook[_page * 2].sprite;
        _rightPage.sprite = _cookBook[_page * 2 + 1].sprite;
        AudioSystem.Instance.PlaySFX("PageTurn");
    }

    public void PreviousPage()
    {
        if (_page == 0)
            return;

        _page--;
        DisplayPage();
    }

    public void NextPage()
    {
        if (_page == _maxPage)
            return;

        _page++;
        DisplayPage();
    }
}