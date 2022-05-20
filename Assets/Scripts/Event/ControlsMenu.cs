using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    
    private const String CONTROLS_MENU_NAME = "Controls Menu";

    public void Exit()
    {
        transform.Find(CONTROLS_MENU_NAME).gameObject.SetActive(false);
    }
}
