using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Slider _musicSlider, _sfxSlider;
    public Image fadeImage;

    public void ToggleMusic() => AudioSystem.Instance.ToggleMusic();
    public void ToggleSFX() => AudioSystem.Instance.ToggleSFX();
    public void MusicVolume() => AudioSystem.Instance.MusicVolume(_musicSlider.value);
    public void SFXVolume() => AudioSystem.Instance.SFXVolume(_sfxSlider.value);

    public IEnumerator Fade(float targetAlpha, float duration)
    {
        Color color = fadeImage.color;
        float startAlpha = color.a;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        fadeImage.color = color;
    }
}