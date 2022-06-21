using System;
using System.Collections;
using System.Collections.Generic;
using Event;
using TMPro;
using UnityEngine;

public class ControlAdvise : MonoBehaviour
{

    [SerializeField] private UIMenu _mainMenu;

    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private KeyCode _keyCode;
    
    private void Start()
    {
        Time.timeScale = 0f;
        _text.SetText("Press " + (char)34 + _keyCode + (char)34 + " to back to game");
    }

    void Update()
    {
        if (Input.GetKey(_keyCode))
        {
            _mainMenu.SetTutorialActive(false);
            Time.timeScale = 1f;
            Destroy(gameObject);
        }    
    }
}
