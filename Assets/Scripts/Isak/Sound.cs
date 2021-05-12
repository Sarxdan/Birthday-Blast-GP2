using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound 
{
    public string name;
    public AudioClip clip;
    [Range(0,1)] public float volume;
    [Range(0,3)] public float pitch;
    public bool loop;
    public bool backgroundMusic;
    [Range(1, 50)]public int amountofObjects;

    [HideInInspector]
    public AudioSource source;

    [HideInInspector]
    public GameObject gameObject;
    [HideInInspector] public float defaultVolume;
}
