using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mixtape : MonoBehaviour
{
    [SerializeField] AudioClip[] tracks;
    [SerializeField] bool muteAudio;
    AudioSource musicPlayer;
    private void Awake() {
        musicPlayer = GetComponent<AudioSource>();
    }

    void RandomizeTrack()
    {
        int randomizedTrackIndex = Random.Range(0, tracks.Length);
        musicPlayer.PlayOneShot(tracks[randomizedTrackIndex]);
    }
    private void Update() {
        if(!musicPlayer.isPlaying)
        {
            RandomizeTrack();
        }
        musicPlayer.mute = muteAudio;
    }
}
