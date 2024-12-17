using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class Sound
{
    public string _name;
    public AudioClip _clip;
    public bool _loop;
    [Range(0, 1)]
    public float _volume = 1;
}

public class AudioSystem : Singleton<AudioSystem>
{
    [Range(0, 1)]
    [SerializeField] private float _musicVolume = 1;
    [Range(0, 1)]
    [SerializeField] private float _sfxVolume = 1;
    [Range(0, 1)]
    [SerializeField] private float _sentenceVolume = 1;
    [SerializeField] private String _startingMusic;
    [SerializeField] private Sound[] _musicSounds, _sfxSounds, _sentences;
    [SerializeField] private AudioSource _musicSource, _sfxSource;

    private Sound _previousMusic, _currentMusic;

    private void Start()
    {
        if (_startingMusic != null) PlayMusic(_startingMusic);
    }

    private void Update()
    {
        if (_currentMusic != null) CheckIsPlaying();
    }

    public void PlayMusic(string name)
    {
        Sound music = Array.Find(_musicSounds, x => x._name == name);

        if (music == null)
            return;

        _currentMusic = music;
        if (_currentMusic._loop) _previousMusic = _currentMusic;

        _musicSource.clip = music._clip;
        _musicSource.volume = music._volume * _musicVolume;
        _musicSource.loop = music._loop;
        _musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound sfx = Array.Find(_sfxSounds, x => x._name == name);

        if (sfx == null)
            return;

        _sfxSource.volume = sfx._volume * _sfxVolume;
        _sfxSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        _sfxSource.PlayOneShot(sfx._clip);
    }

    public void PlaySentence(string name, AudioSource source)
    {
        Sound sentence = Array.Find(_sentences, x => x._name == name);

        if (sentence == null)
            return;

        source.volume = sentence._volume * _sentenceVolume;
        source.PlayOneShot(sentence._clip);
    }

    public void PlaySFX(string name, Vector3 location)
    {
        Sound sfx = Array.Find(_sfxSounds, x => x._name == name);

        if (sfx == null)
            return;

        PlayClipAt(sfx._clip, location, sfx._volume * _sfxVolume);
    }

    public void ToggleMusic() => _musicSource.mute = !_musicSource.mute;
    public void ToggleSFX() => _sfxSource.mute = !_sfxSource.mute;
    public void MusicVolume(float volume)
    {
        _musicVolume = volume;
        _musicSource.volume = _musicVolume * _currentMusic._volume;
    }
    public void SFXVolume(float volume)
    {
        _sfxVolume = volume;
        _sfxSource.volume = _sfxVolume;
    }

    private void CheckIsPlaying()
    {
        if (!_musicSource.isPlaying)
        {
            PlayMusic(_previousMusic._name);
        }
    }

    public void PlayClipAt(AudioClip clip, Vector3 pos, float volume) {
        GameObject tempGO = new GameObject("TempAudioGo");
        tempGO.transform.position = pos;
        AudioSource aSource = tempGO.AddComponent<AudioSource>();

        aSource.clip = clip;
        aSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        aSource.volume = volume;    
        aSource.Play();
        Destroy(tempGO, clip.length);
    }

    public void PlayMusicWithDelay(float delay, string name)
    {
        StartCoroutine(MusicWithDelay(delay, name));
    }

    public void PlaySFXWithDelay(float delay, string name)
    {
        StartCoroutine(SFXWithDelay(delay, name));
    }
        
    public void PlaySFXWithDelay(float delay, string name, Vector3 location)
    {
        StartCoroutine(SFXWithDelay(delay, name, location));
    }

    IEnumerator MusicWithDelay(float delay, string name)
    {
        yield return new WaitForSeconds(delay);
        PlayMusic(name);
    }
    IEnumerator SFXWithDelay(float delay, string name)
    {
        yield return new WaitForSeconds(delay);
        PlaySFX(name);
    }

    IEnumerator SFXWithDelay(float delay, string name, Vector3 location)
    {
        yield return new WaitForSeconds(delay);
        PlaySFX(name, location);
    }
}