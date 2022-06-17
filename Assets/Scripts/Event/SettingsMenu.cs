using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Event
{
    public class SettingsMenu : MonoBehaviour
    {
    
        [SerializeField] private String _musicName;
        private const String SETTINGS_MENU_NAME = "Settings Menu";
        
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Toggle _muteToggle;
        [SerializeField] private Toggle _tutorialToggle;
    
        private float _lastVolumeValue;

        private void Update()
        {
            if (_tutorialToggle != null)
            {
                _tutorialToggle.isOn = SceneChangerManager.Instance.GetActivateTutorial();
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
                _audioMixer.SetFloat(_musicName, -80);
                _muteToggle.isOn = true;
            }
            else
            {
                _audioMixer.SetFloat(_musicName, volume);
                _muteToggle.isOn = false;
            }
            _lastVolumeValue = volume;
            
        }
    
        public void Mute(bool mute)
        {
            if (mute)
            {
                _audioMixer.SetFloat(_musicName, -80);
            }
            else
            {
                _audioMixer.SetFloat(_musicName, _lastVolumeValue);
            }
        }

        public void SetTutorial(bool activateTutorial)
        {
            SceneChangerManager.Instance.SetActivateTutorial(activateTutorial);
        }

        public void Exit()
        {
            transform.Find(SETTINGS_MENU_NAME).gameObject.SetActive(false);
        }
    }
}

