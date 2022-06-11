using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Event
{
    public class LevelsMenu : MonoBehaviour
    {
        
        private const String LEVELS_MENU_NAME = "Levels Menu";


        public void LevelButton(int levelIndex)
        {
            Destroy(GameObject.FindWithTag("Music"));
            SceneChangerManager.Instance.SetScore(0);
            SceneManager.LoadScene(levelIndex);
        }

        public void Exit()
        {
            transform.Find(LEVELS_MENU_NAME).gameObject.SetActive(false);
        }
    }
}

