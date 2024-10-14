using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private Slider _musicSlider, _sfxSlider;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void ToggleMusic() => AudioController.Instance.ToggleMusic();
    public void ToggleSFX() => AudioController.Instance.ToggleSFX();
    public void MusicVolume() => AudioController.Instance.MusicVolume(_musicSlider.value);
    public void SFXVolume() => AudioController.Instance.SFXVolume(_sfxSlider.value);
}
