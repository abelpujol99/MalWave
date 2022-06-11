using System;
using System.Collections;
using System.Collections.Generic;
using Event;
using TMPro;
using UnityEngine;

public class ControlAdvise : MonoBehaviour
{

    [SerializeField] private UIMenu _mainMenu;
    private void Start()
    {
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            _mainMenu.SetTutorialActive(false);
            Time.timeScale = 1f;
            Destroy(gameObject);
        }    
    }
}
