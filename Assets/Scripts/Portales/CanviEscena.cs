using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEditor.SearchService.Scene;



public class CanviEscena : MonoBehaviour
{
    [SerializeField] private int _nextSceneIndex;

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        
        if (col.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex == 2 && SceneChangerManager.GetLastSceneIndex() == 1 )
                _nextSceneIndex = 5;
            else if (SceneManager.GetActiveScene().buildIndex == 5 && SceneChangerManager.GetLastSceneIndex() == 2)
                _nextSceneIndex = 3;
            else if (SceneManager.GetActiveScene().buildIndex == 3 && SceneChangerManager.GetLastSceneIndex() == 5)
                _nextSceneIndex = 6;
            //else if (SceneManager.GetActiveScene().buildIndex == 6 && SceneChangerManager.GetLastSceneIndex() == 3)
                //_nextSceneIndex = 4;
            else if (SceneManager.GetActiveScene().buildIndex == 6 && SceneChangerManager.GetLastSceneIndex() == 3)
                _nextSceneIndex = 7;
            
            if (SceneManager.GetActiveScene().buildIndex == 3 && SceneChangerManager.GetLastSceneIndex() == 1)
                _nextSceneIndex = 5;
            //else if (SceneManager.GetActiveScene().buildIndex == 5 && SceneChangerManager.GetLastSceneIndex() == 3)
                //_nextSceneIndex = 4;
            else if (SceneManager.GetActiveScene().buildIndex == 5 && SceneChangerManager.GetLastSceneIndex() == 3)
                _nextSceneIndex = 2;
            else if (SceneManager.GetActiveScene().buildIndex == 2 && SceneChangerManager.GetLastSceneIndex() == 5)
                _nextSceneIndex = 6;
            else if (SceneManager.GetActiveScene().buildIndex == 6 && SceneChangerManager.GetLastSceneIndex() == 2)
                _nextSceneIndex = 7;
            
            /*if (SceneManager.GetActiveScene().buildIndex == 4 && SceneChangerManager.GetLastSceneIndex() == 1)
                _nextSceneIndex = 5;
            else if (SceneManager.GetActiveScene().buildIndex == 5 && SceneChangerManager.GetLastSceneIndex() == 4)
                _nextSceneIndex = 2;
            else if (SceneManager.GetActiveScene().buildIndex == 2 && SceneChangerManager.GetLastSceneIndex() == 5)
                _nextSceneIndex = 6;
            else if (SceneManager.GetActiveScene().buildIndex == 6 && SceneChangerManager.GetLastSceneIndex() == 2)
                _nextSceneIndex = 3;
            else if (SceneManager.GetActiveScene().buildIndex == 3 && SceneChangerManager.GetLastSceneIndex() == 6)
                _nextSceneIndex = 7;*/
            
            SceneChangerManager.SetLastSceneIndex(SceneManager.GetActiveScene().buildIndex);
            
            SceneManager.LoadScene(_nextSceneIndex);
        }
    }
}
