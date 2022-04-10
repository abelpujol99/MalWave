using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _settingsMenu;
    
    

    // Update is called once per frame
    private void Update()
    {
        if (!_settingsMenu.activeSelf)
        {
            _mainPanel.SetActive(true);
        }
    }

    public void PressPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
