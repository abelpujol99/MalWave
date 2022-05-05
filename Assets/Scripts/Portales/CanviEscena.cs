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
    
    private int _lastSceneIndex;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        
        if (col.gameObject.tag == "Player")
        {
            if (SceneManager.GetActiveScene().buildIndex == 2 && _lastSceneIndex == 1 )
                _nextSceneIndex = 5;
            else if (SceneManager.GetActiveScene().buildIndex == 5 && _lastSceneIndex == 2)
                _nextSceneIndex = 3;
            else if (SceneManager.GetActiveScene().buildIndex == 3 && _lastSceneIndex == 5)
                _nextSceneIndex = 6;
            //else if (SceneManager.GetActiveScene().buildIndex == 6 && _lastSceneIndex == 3)
                //_nextSceneIndex = 4;
            else if (SceneManager.GetActiveScene().buildIndex == 6 && _lastSceneIndex == 3)
                _nextSceneIndex = 7;
            
            if (SceneManager.GetActiveScene().buildIndex == 3 && _lastSceneIndex == 1)
                _nextSceneIndex = 5;
            //else if (SceneManager.GetActiveScene().buildIndex == 5 && _lastSceneIndex == 3)
                //_nextSceneIndex = 4;
            else if (SceneManager.GetActiveScene().buildIndex == 5 && _lastSceneIndex == 3)
                _nextSceneIndex = 2;
            else if (SceneManager.GetActiveScene().buildIndex == 2 && _lastSceneIndex == 5)
                _nextSceneIndex = 6;
            else if (SceneManager.GetActiveScene().buildIndex == 6 && _lastSceneIndex == 2)
                _nextSceneIndex = 7;
            
            /*if (SceneManager.GetActiveScene().buildIndex == 4 && _lastSceneIndex == 1)
                _nextSceneIndex = 5;
            else if (SceneManager.GetActiveScene().buildIndex == 5 && _lastSceneIndex == 4)
                _nextSceneIndex = 2;
            else if (SceneManager.GetActiveScene().buildIndex == 2 && _lastSceneIndex == 5)
                _nextSceneIndex = 6;
            else if (SceneManager.GetActiveScene().buildIndex == 6 && _lastSceneIndex == 2)
                _nextSceneIndex = 3;
            else if (SceneManager.GetActiveScene().buildIndex == 3 && _lastSceneIndex == 6)
                _nextSceneIndex = 7;*/
            
            
            DontDestroyOnLoad(gameObject);
            _lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            SceneManager.LoadScene(_nextSceneIndex);
        }
    }
}
