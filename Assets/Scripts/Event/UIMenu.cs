using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    private const String MAIN_MENU_BUTTON_NAME = "Main Menu";
    
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _deathMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private Player _player;

    private GameObject _lastActiveMenu;

    private bool _pause;
    private bool _dead;

    // Update is called once per frame
    private void Update()
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
            if (_player.GetDeah())
            {
                _deathMenu.SetActive(true);
            }
            else
            { 
                CheckPause(); 
            }
        }
    }

    void CheckPause()
    {
        if (Input.GetKeyDown(KeyCode.P))
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
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        _pause = true;
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void MainMenu()
    {
        _pause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(MAIN_MENU_BUTTON_NAME);
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

}
