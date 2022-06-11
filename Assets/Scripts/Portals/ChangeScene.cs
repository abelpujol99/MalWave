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

        #region FirstPortal
        if (activeScene == 9 && _lastSceneIndex == 8)
        {
            _nextSceneIndex = 12;
        }
        else if (activeScene == 12 && _lastSceneIndex == 9)
        {
            _nextSceneIndex = 10;
        }
        else if (activeScene == 10 && _lastSceneIndex == 12)
        {
            _nextSceneIndex = 13;
        }
        else if (activeScene == 13 && _lastSceneIndex == 10)
        {
            _nextSceneIndex = 11;
        }
        else if (activeScene == 11 && _lastSceneIndex == 13)
        {
            _nextSceneIndex = 14;
        }
        #endregion
        
        #region SecondPortal
        if (activeScene == 10 && _lastSceneIndex == 8)
        {
            _nextSceneIndex = 12;
        }
        else if (activeScene == 12 && _lastSceneIndex == 10)
        {
            _nextSceneIndex = 11;
        }
        else if (activeScene == 11 && _lastSceneIndex == 12)
        {
            _nextSceneIndex = 13;   
        }
        else if (activeScene == 13 && _lastSceneIndex == 11)
        {
            _nextSceneIndex = 9;
        }
        else if (activeScene == 9 && _lastSceneIndex == 13)
        {
            _nextSceneIndex = 14;    
        }
        #endregion
        
        #region ThirdPortal
        if (activeScene == 11 && _lastSceneIndex == 8)
        {
            _nextSceneIndex = 12;
        }
        else if (activeScene == 12 && _lastSceneIndex == 11)
        {
            _nextSceneIndex = 9;    
        }
        else if (activeScene == 9 && _lastSceneIndex == 12)
        {
            _nextSceneIndex = 13;    
        }
        else if (activeScene == 13 && _lastSceneIndex == 9)
        {
            _nextSceneIndex = 10;    
        }
        else if (activeScene == 10 && _lastSceneIndex == 13)
        {
            _nextSceneIndex = 14;    
        }
        #endregion
        
        #endregion

        ChoosePortalColor();

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
            switch (_nextSceneIndex)
            {
                case 2:
                    _spriteRenderer.sprite = _cyanPortal;
                    break;
                case 3:
                    _spriteRenderer.sprite = _pinkPortal;
                    break;
                case 4:
                    _spriteRenderer.sprite = _purplePortal;
                    break;
                case 5:
                    _spriteRenderer.sprite = _bluePortal;
                    break;
                case 6:
                    _spriteRenderer.sprite = _greenPortal;
                    break;
                case 7:
                    _spriteRenderer.sprite = _redPortal;
                    break;
            }
        }
        else
        {
            _spriteRenderer.flipX = true;
            switch (_lastSceneIndex)
            {
                case 1:
                case 6:
                case 8:
                case 12:
                    _spriteRenderer.sprite = _greenPortal;
                    break;
                
                case 2:
                case 11:
                    _spriteRenderer.sprite = _cyanPortal;
                    break;
                
                case 3:
                case 13: 
                    _spriteRenderer.sprite = _pinkPortal;
                    break;
                
                case 4:
                case 9:
                    _spriteRenderer.sprite = _purplePortal;
                    break;
                
                case 5:
                case 10:
                    _spriteRenderer.sprite = _bluePortal;
                    break;
            }
        }
    }
}
