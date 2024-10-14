using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.PlayerSettings;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    [Range(0, 1)]
    [SerializeField] private float _musicVolume = 1;
    [Range(0, 1)]
    [SerializeField] private float _sfxVolume = 1;
    [SerializeField] private String _startingMusic;
    [SerializeField] private Sound[] _musicSounds, _sfxSounds;
    [SerializeField] private AudioSource _musicSource, _sfxSource;

    private Sound _previousMusic, _currentMusic;

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

        if (music != null)
        {
            _currentMusic = music;
            if (_currentMusic._loop) _previousMusic = _currentMusic;

            _musicSource.clip = music._clip;
            _musicSource.volume = music._volume * _musicVolume;
            _musicSource.loop = music._loop;
            _musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sfx = Array.Find(_sfxSounds, x => x._name == name);
        if (sfx != null)
        {
            _sfxSource.volume = sfx._volume * _sfxVolume;
            _sfxSource.PlayOneShot(sfx._clip);
        }
    }

    public void PlaySFX(string name, Vector3 location)
    {
        Sound sfx = Array.Find(_sfxSounds, x => x._name == name);
        if (sfx != null)
        {
            PlayClipAt(sfx._clip, location, sfx._volume * _sfxVolume);
        }
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
        aSource.volume = volume;    
        aSource.Play();
        Destroy(tempGO, clip.length);
    }
}