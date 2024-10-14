using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string _name;
    public AudioClip _clip;
    public bool _loop;
    [Range(0, 1)]
    public float _volume = 1;
}
