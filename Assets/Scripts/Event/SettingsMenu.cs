using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Event
{
    public class SettingsMenu : MonoBehaviour
    {
    
        private const String MUSIC_VOLUME_MIXER_NAME = "Music Volume";
        private const String SETTINGS_MENU_NAME = "Settings Menu";
        
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Toggle _muteToggle;
    
        private float _lastVolumeValue;
    
        private void Update()
        {
            if (_lastVolumeValue < -50.0f)
            {
                _audioMixer.SetFloat(MUSIC_VOLUME_MIXER_NAME, -80);
                _muteToggle.isOn = true;
            }
        }
    
        public void FullScreenToggle(bool fullScreen)
        {
            Screen.fullScreen = fullScreen;
        }
    
        public void SetVolume(float volume)
        {
            if (volume < -50.0f)
            {
                _audioMixer.SetFloat(MUSIC_VOLUME_MIXER_NAME, -80);
                _muteToggle.isOn = true;
            }
            else
            {
                _audioMixer.SetFloat(MUSIC_VOLUME_MIXER_NAME, volume);
                _muteToggle.isOn = false;
            }
            _lastVolumeValue = volume;
            
        }
    
        public void Mute(bool mute)
        {
            if (mute)
            {
                _audioMixer.SetFloat(MUSIC_VOLUME_MIXER_NAME, -80);
            }
            else
            {
                _audioMixer.SetFloat(MUSIC_VOLUME_MIXER_NAME, _lastVolumeValue);
            }
        }
    
        public void Exit()
        {
            transform.Find(SETTINGS_MENU_NAME).gameObject.SetActive(false);
        }
    }
}

