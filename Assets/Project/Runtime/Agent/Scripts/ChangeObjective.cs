using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ChangeObjective : MonoBehaviour
{
    [SerializeField] private FollowObjective _book;
    [SerializeField] private Transform _backObjective, _frontObjective;
    private StarterAssetsInputs _input;
    private bool _isShowingBook;


    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        ShowBook();
    }

    private void ShowBook()
    {
        if (_input.showBook)
        {
            _book.ChangeObjective(_isShowingBook ? _frontObjective : _backObjective);
            _isShowingBook = !_isShowingBook;
            _input.showBook = false;
        }
    }
}
