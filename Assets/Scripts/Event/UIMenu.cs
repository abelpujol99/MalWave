using System;
using Characters.Main;
using Event;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Event
{
    public class UIMenu : MonoBehaviour
    {
        private const String MAIN_MENU_BUTTON_NAME = "Main Menu";

        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _deathMenu;
        [SerializeField] private GameObject _settingsMenu;
        [SerializeField] private GameObject _scorePanel;
        [SerializeField] private GameObject _winMenu;

        [SerializeField] private TextMeshProUGUI _killText;
        [SerializeField] private TextMeshProUGUI _winText;
        [SerializeField] private TextMeshProUGUI _pauseScoreText;
        [SerializeField] private TextMeshProUGUI _deathScoreText;

        [SerializeField] private Player _player;

        private GameObject _lastActiveMenu;

        private bool _pause;
        private bool _tutorialActive;

        // Update is called once per frame
        private void Update()
        {
            if (_player.GetWin())
            {
                _winMenu.SetActive(true);
                if (SceneManager.GetActiveScene().buildIndex == 15) return;
                _winText.SetText("YOU WIN\nSCORE: " + SceneChangerManager.Instance.GetScore());
            }
            else
            {
                if (_settingsMenu.activeSelf)
                {
                    CheckLastActiveMenuAndDeactivate();
                }
                else if (_lastActiveMenu != null)
                {
                    _lastActiveMenu.SetActive(true);
                    _lastActiveMenu = null;
                }
                else
                {
                    if (_player.GetDeath())
                    {
                        _deathMenu.SetActive(true);
                        _killText.SetText(_player.GetKiller().ToUpper() + "\nKILLED YOU");
                        _deathScoreText.SetText("SCORE: " + SceneChangerManager.Instance.GetScore());
                    }
                    else
                    {
                        CheckPause();
                    }
                }
            }

        }

        private void CheckPause()
        {
            if (Input.GetKeyDown(KeyCode.P) && !_tutorialActive)
            {
                if (_pause)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            _pause = false;
            _pauseMenu.SetActive(false);
            _scorePanel.SetActive(true);
            Time.timeScale = 1f;
        }

        private void Pause()
        {
            _pause = true;
            _pauseMenu.SetActive(true);
            _pauseScoreText.SetText("SCORE: " + SceneChangerManager.Instance.GetScore());
            _scorePanel.SetActive(false);
            Time.timeScale = 0f;
        }

        public void Restart(int restartToLevel)
        {
            SceneManager.LoadScene(restartToLevel);
            Destroy(GameObject.FindWithTag("Music"));
            SceneChangerManager.Instance.SetScore(0);
            Resume();
        }

        public void MainMenu()
        {
            _pause = false;
            Time.timeScale = 1f;
            Destroy(GameObject.FindWithTag("Music"));
            SceneChangerManager.Instance.SetScore(0);
            SceneManager.LoadScene(MAIN_MENU_BUTTON_NAME);
        }

        public void LevelButton(int levelIndex)
        {
            Destroy(GameObject.FindWithTag("Music"));
            SceneChangerManager.Instance.SetScore(0);
            SceneManager.LoadScene(levelIndex);
        }

        public void Settings()
        {
            _settingsMenu.SetActive(true);
        }

        private void CheckLastActiveMenuAndDeactivate()
        {
            if (_pauseMenu.activeSelf)
            {
                _pauseMenu.SetActive(false);
                _lastActiveMenu = _pauseMenu;
            }
            else if (_deathMenu.activeSelf)
            {
                _deathMenu.SetActive(false);
                _lastActiveMenu = _deathMenu;
            }
        }

        public void SetTutorialActive(bool tutorialActive)
        {
            _tutorialActive = tutorialActive;
        }

    }
}

