using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Event
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _levelsMenu;
        [SerializeField] private GameObject _controlsPanel;
        [SerializeField] private GameObject _settingsMenu;
        [SerializeField] private Button _level1Button;
        [SerializeField] private Button _bossButton;

        private void Start()
        {
            if (SceneChangerManager.Instance.GetCompleteTutorial())
            {
                _level1Button.interactable = true;
            }

            if (SceneChangerManager.Instance.GetCompleteLevel1())
            {
                _bossButton.interactable = true;
            }
        }

        private void Update()
        {
            if (!_settingsMenu.activeSelf && !_controlsPanel.activeSelf && !_levelsMenu.activeSelf)
            {
                _mainPanel.SetActive(true);
            }
        }

        public void PressPlay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void LevelsMenu()
        {
            _mainPanel.SetActive(false);
            _levelsMenu.SetActive(true);
        }

        public void ControlsMenu()
        {
            _mainPanel.SetActive(false);
            _controlsPanel.SetActive(true);
        }

        public void SettingsMenu()
        {
            _mainPanel.SetActive(false);
            _settingsMenu.SetActive(true);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}

