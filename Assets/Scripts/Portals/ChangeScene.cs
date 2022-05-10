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
            #region FirstPortal
            if (SceneManager.GetActiveScene().buildIndex == 2 && SceneChangerManager.Instance.GetLastSceneIndex() == 1)
            {
                _nextSceneIndex = 5;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 5 && SceneChangerManager.Instance.GetLastSceneIndex() == 2)
            {
                _nextSceneIndex = 3;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3 && SceneChangerManager.Instance.GetLastSceneIndex() == 5)
            {
                _nextSceneIndex = 6;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 6 && SceneChangerManager.Instance.GetLastSceneIndex() == 3)
            {
                _nextSceneIndex = 4;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 4 && SceneChangerManager.Instance.GetLastSceneIndex() == 6)
            {
                _nextSceneIndex = 7;
            }
            #endregion

            #region SecondPortal
            if (SceneManager.GetActiveScene().buildIndex == 3 && SceneChangerManager.Instance.GetLastSceneIndex() == 1)
            {
                _nextSceneIndex = 5;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 5 && SceneChangerManager.Instance.GetLastSceneIndex() == 3)
            {
                _nextSceneIndex = 4;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 4 && SceneChangerManager.Instance.GetLastSceneIndex() == 5)
            {
                _nextSceneIndex = 6;   
            }
            else if (SceneManager.GetActiveScene().buildIndex == 6 && SceneChangerManager.Instance.GetLastSceneIndex() == 4)
            {
                _nextSceneIndex = 2;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2 && SceneChangerManager.Instance.GetLastSceneIndex() == 6)
            {
                _nextSceneIndex = 7;    
            }
            #endregion

            #region ThirdPortal
            if (SceneManager.GetActiveScene().buildIndex == 4 && SceneChangerManager.Instance.GetLastSceneIndex() == 1)
            {
                _nextSceneIndex = 5;
            }
            else if (SceneManager.GetActiveScene().buildIndex == 5 && SceneChangerManager.Instance.GetLastSceneIndex() == 4)
            {
                _nextSceneIndex = 2;    
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2 && SceneChangerManager.Instance.GetLastSceneIndex() == 5)
            {
                _nextSceneIndex = 6;    
            }
            else if (SceneManager.GetActiveScene().buildIndex == 6 && SceneChangerManager.Instance.GetLastSceneIndex() == 2)
            {
                _nextSceneIndex = 3;    
            }
            else if (SceneManager.GetActiveScene().buildIndex == 3 && SceneChangerManager.Instance.GetLastSceneIndex() == 6)
            {
                _nextSceneIndex = 7;    
            }
            #endregion

            ChoosePortalColor();

            SceneChangerManager.Instance.SetLastSceneIndex(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
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
