using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    private const String MUSIC_VOLUME_MIXER_NAME = "Music Volume";
    private const String SETTINGS_MENU_NAME = "Settings Menu";
    
    [SerializeField] private AudioMixer _audioMixer;
    
    public void FullScreenToggle(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat(MUSIC_VOLUME_MIXER_NAME, volume);
    }

    public void Exit()
    {
        transform.Find(SETTINGS_MENU_NAME).gameObject.SetActive(false);
    }
}
