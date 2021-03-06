using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] Sound[] sounds;

    public void ChangeBGMAudio(float value) 
    {
        foreach(Sound s in sounds)
        {
            if(s.backgroundMusic) s.source.volume = s.defaultVolume * value;
        }
    }

    public void ChangeFXAudio(float value) 
    {
        foreach(Sound s in sounds)
        {
            if(!s.backgroundMusic) s.source.volume = s.defaultVolume * value;
        }
        Play("SettingsFX");
    }
    protected override void Awake() {
        base.Awake();
        foreach(Sound s in sounds)
        {

            GameObject obj = new GameObject();
            obj.name = s.name + " track(s)";
            obj.transform.parent = gameObject.transform;

            for(int i = 0; i < s.amountofObjects; i++)
            {

                GameObject child = new GameObject();
                child.transform.parent = obj.gameObject.transform;
                s.source = child.AddComponent<AudioSource>();

                s.gameObject = child;
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.defaultVolume = s.source.volume;

            }
        }
    }
    private void Start() {
        Play("BGM");
    }

    public AudioSource PlayClipAtPoint(string name, Vector3 position) //will play in 3D
    {
        AudioSource source;
        Transform parent = FindTrackParent(name);
        if (parent == null)
        {
            Debug.LogError("Sound: " + name + "not found!");
            return null;
        }
        foreach (Transform child in parent)
        {
            source = child.GetComponent<AudioSource>();
            if(!source.isPlaying)
            {
                source.spatialBlend = 1;
                child.gameObject.transform.position = position;
                source.Play();
                return source; // if a source which is nopt playing is found, play and return it
            }
        }
        Transform firstChild = parent.GetChild(0).transform;
        source = firstChild.GetComponent<AudioSource>();
        firstChild.gameObject.transform.position = position; 
        source.Play();
        return source; // if all sources are currently active, reuse the first child
    }

    private Transform FindTrackParent(string name)
    {
        foreach (Transform child in transform)
        {
            if (child.name == name + " track(s)")
            {
                return child;
            }
        }
        return null;
    }

    public bool IsPlaying(string name) //if checking while paused, the audio will still play
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogError("Sound: " + name + "not found!");
            return false;
        } 
        return s.source.isPlaying;
        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogError("Sound: " + name + " not found!");
            return;
        } 
        s.source.spatialBlend = 0;
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogError("Sound: " + name + "not found!");
            return;
        } 
        if(s.source.isPlaying) s.source.Stop();
        
    }

    void StopAllAudio()
    {
        foreach(Sound s in sounds)
        {
            if(!s.backgroundMusic) s.source.Stop(); //dont stop BGM
        }
    }

    void OnGameStateChange(Gamemanager.GameState newState)
    {
        switch(newState)
        {
            case Gamemanager.GameState.Paused:
            foreach(Sound s in sounds)
        {
            if(!s.backgroundMusic) s.source.Pause(); //dont pause BGM
        }
            break;

            case Gamemanager.GameState.Pregame:
                foreach(Sound s in sounds)
        {
            if(!s.backgroundMusic) s.source.Stop();
        }
            break;

            case Gamemanager.GameState.Playing:
                foreach(Sound s in sounds)
        {
            s.source.UnPause();
        }
            break;

        }
    }

    private void OnEnable() {
        Gamemanager.onGameStateChange += OnGameStateChange;
        Gamemanager.onSceneLoaded += StopAllAudio;
    }

    private void OnDisable() {
        Gamemanager.onGameStateChange -= OnGameStateChange;
        Gamemanager.onSceneLoaded -= StopAllAudio;
    }
}
