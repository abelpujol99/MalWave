using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlAdvise : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Time.timeScale = 1f;
            Destroy(gameObject);
        }    
    }
}
