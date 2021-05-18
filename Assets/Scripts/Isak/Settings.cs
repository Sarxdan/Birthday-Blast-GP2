using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Slider masterVolume;
    [SerializeField] Slider soundFX;
    [SerializeField] Slider music;
    [SerializeField] Slider mouseSensitivity;
    [SerializeField] Toggle invertMouse;

    // Start is called before the first frame update
    void Start()
    {
        masterVolume.onValueChanged.AddListener(masterVolumeChanged);
        soundFX.onValueChanged.AddListener(soundFXChanged);
        music.onValueChanged.AddListener(musicChanged);
        mouseSensitivity.onValueChanged.AddListener(mouseSensitivityChanged);
    }

    void masterVolumeChanged(float value)
    {
        AudioListener.volume = value;
    }

    void soundFXChanged(float value)
    {
        AudioManager.instance.ChangeFXAudio(value);
    }

    void musicChanged(float value)
    {
        AudioManager.instance.ChangeBGMAudio(value);
    }

    void mouseSensitivityChanged(float value)
    {
        PlayerManager.instance.OnMouseSensitivityChanged(value);
    }

    public void InvertMouse()
    {
        PlayerManager.instance.OnMouseInverted(invertMouse.isOn);
    }
}
