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

    private void Start()
    {

        int activeScene = SceneManager.GetActiveScene().buildIndex;
        int lastActiveScene = SceneChangerManager.Instance.GetLastSceneIndex();
        
        #region Tutorial
        
        #region FirstPortal
        if (activeScene == 2 && lastActiveScene == 1)
        {
            _nextSceneIndex = 5;
        }
        else if (activeScene == 5 && lastActiveScene == 2)
        {
            _nextSceneIndex = 3;
        }
        else if (activeScene == 3 && lastActiveScene == 5)
        {
            _nextSceneIndex = 6;
        }
        else if (activeScene == 6 && lastActiveScene == 3)
        {
            _nextSceneIndex = 4;
        }
        else if (activeScene == 4 && lastActiveScene == 6)
        {
            _nextSceneIndex = 7;
        }
        #endregion

        #region SecondPortal
        if (activeScene == 3 && lastActiveScene == 1)
        {
            _nextSceneIndex = 5;
        }
        else if (activeScene == 5 && lastActiveScene == 3)
        {
            _nextSceneIndex = 4;
        }
        else if (activeScene == 4 && lastActiveScene == 5)
        {
            _nextSceneIndex = 6;   
        }
        else if (activeScene == 6 && lastActiveScene == 4)
        {
            _nextSceneIndex = 2;
        }
        else if (activeScene == 2 && lastActiveScene == 6)
        {
            _nextSceneIndex = 7;    
        }
        #endregion

        #region ThirdPortal
        if (activeScene == 4 && lastActiveScene == 1)
        {
            _nextSceneIndex = 5;
        }
        else if (activeScene == 5 && lastActiveScene == 4)
        {
            _nextSceneIndex = 2;    
        }
        else if (activeScene == 2 && lastActiveScene == 5)
        {
            _nextSceneIndex = 6;    
        }
        else if (activeScene == 6 && lastActiveScene == 2)
        {
            _nextSceneIndex = 3;    
        }
        else if (activeScene == 3 && lastActiveScene == 6)
        {
            _nextSceneIndex = 7;    
        }
        #endregion
        
        #endregion
        
        #region Level1
        
        
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
        if (_nextSceneIndex == 2)
        {
            _spriteRenderer.sprite = _cyanPortal;
        }
        else if(_nextSceneIndex == 3)
        {
            _spriteRenderer.sprite = _pinkPortal;
        }
        else if (_nextSceneIndex == 4)
        {
            _spriteRenderer.sprite = _purplePortal;
        }
        else if (_nextSceneIndex == 5)
        {
            _spriteRenderer.sprite = _bluePortal;
        }
        else if (_nextSceneIndex == 6)
        {
            _spriteRenderer.sprite = _greenPortal;
        }
        else if (_nextSceneIndex == 7)
        {
            _spriteRenderer.sprite = _redPortal;
        }
    }
}
