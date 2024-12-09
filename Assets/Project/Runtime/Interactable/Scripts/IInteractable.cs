using System;
using UnityEngine;

public interface IInteractable
{
    public string InteractPrompt { get;}
    public GameObject PromptPrefab { get;}
    public Transform PromptLocation { get;}
    public bool IsDisplay { get;}

    public bool Interact(Interactor interactor);
    public void SetupPrompt(bool show);
}