using System;
using UnityEngine;

namespace Event
{
    public class CreditsMenu : MonoBehaviour
    {
    
        private const String CREDITS_MENU_NAME = "Credits Menu";

        public void Exit()
        {
            transform.Find(CREDITS_MENU_NAME).gameObject.SetActive(false);
        }
    }
}

