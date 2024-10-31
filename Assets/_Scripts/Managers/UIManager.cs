using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Slider _musicSlider, _sfxSlider;

    public void ToggleMusic() => AudioSystem.Instance.ToggleMusic();
    public void ToggleSFX() => AudioSystem.Instance.ToggleSFX();
    public void MusicVolume() => AudioSystem.Instance.MusicVolume(_musicSlider.value);
    public void SFXVolume() => AudioSystem.Instance.SFXVolume(_sfxSlider.value);
}