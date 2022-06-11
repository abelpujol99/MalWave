using System;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ChangeScene : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private Sprite _orangePortal;
    [SerializeField] private Sprite _bluePortal;
    [SerializeField] private Sprite _cyanPortal;
    [SerializeField] private Sprite _greenPortal;
    [SerializeField] private Sprite _pinkPortal;
    [SerializeField] private Sprite _purplePortal;
    [SerializeField] private Sprite _redPortal;
    [SerializeField] private Sprite _yellowPortal;

    [SerializeField] private int _nextSceneIndex;
    private int _lastSceneIndex;

    [SerializeField] private bool _firstPortalOfTheLevel; 

    private void Start()
    {

        _lastSceneIndex = SceneChangerManager.Instance.GetLastSceneIndex();
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        
        #region Tutorial
        
        #region FirstPortal
        if (activeScene == 2 && _lastSceneIndex == 1)
        {
            _nextSceneIndex = 5;
        }
        else if (activeScene == 5 && _lastSceneIndex == 2)
        {
            _nextSceneIndex = 3;
        }
        else if (activeScene == 3 && _lastSceneIndex == 5)
        {
            _nextSceneIndex = 6;
        }
        else if (activeScene == 6 && _lastSceneIndex == 3)
        {
            _nextSceneIndex = 4;
        }
        else if (activeScene == 4 && _lastSceneIndex == 6)
        {
            _nextSceneIndex = 7;
        }
        #endregion

        #region SecondPortal
        if (activeScene == 3 && _lastSceneIndex == 1)
        {
            _nextSceneIndex = 5;
        }
        else if (activeScene == 5 && _lastSceneIndex == 3)
        {
            _nextSceneIndex = 4;
        }
        else if (activeScene == 4 && _lastSceneIndex == 5)
        {
            _nextSceneIndex = 6;   
        }
        else if (activeScene == 6 && _lastSceneIndex == 4)
        {
            _nextSceneIndex = 2;
        }
        else if (activeScene == 2 && _lastSceneIndex == 6)
        {
            _nextSceneIndex = 7;    
        }
        #endregion

        #region ThirdPortal
        if (activeScene == 4 && _lastSceneIndex == 1)
        {
            _nextSceneIndex = 5;
        }
        else if (activeScene == 5 && _lastSceneIndex == 4)
        {
            _nextSceneIndex = 2;    
        }
        else if (activeScene == 2 && _lastSceneIndex == 5)
        {
            _nextSceneIndex = 6;    
        }
        else if (activeScene == 6 && _lastSceneIndex == 2)
        {
            _nextSceneIndex = 3;    
        }
        else if (activeScene == 3 && _lastSceneIndex == 6)
        {
            _nextSceneIndex = 7;    
        }
        #endregion
        
        #endregion
        
        #region Level1
        
        
        #endregion

        ChoosePortalColor();
        Debug.Log(SceneChangerManager.Instance.GetLastSceneIndex());

        SceneChangerManager.Instance.SetLastSceneIndex(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(_nextSceneIndex);
        }
    }

    private void ChoosePortalColor()
    {
        if (!_firstPortalOfTheLevel)
        {
            _spriteRenderer.sprite = _nextSceneIndex switch
            {
                2 => _cyanPortal,
                3 => _pinkPortal,
                4 => _purplePortal,
                5 => _bluePortal,
                6 => _greenPortal,
                7 => _redPortal,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        else
        {
            _spriteRenderer.flipX = true;
            _spriteRenderer.sprite = SceneChangerManager.Instance.GetLastSceneIndex() switch
            {
                1 => _greenPortal,
                2 => _cyanPortal,
                3 => _pinkPortal,
                4 => _purplePortal,
                5 => _bluePortal,
                6 => _greenPortal,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

    }
}
