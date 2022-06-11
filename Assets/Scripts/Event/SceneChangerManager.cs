using UnityEngine;


namespace Event
{
    public class SceneChangerManager : MonoBehaviour
    {
        private static SceneChangerManager _instance;

        public static SceneChangerManager Instance
        {
            get
            {
                return _instance;
            }
        }

    private int _lastSceneIndex;
    private int _score = 0;
    private bool _deactivateTutorial;
    private void Awake()
    {
        if (_instance == null)
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public int GetLastSceneIndex()
        {
            return _lastSceneIndex;
        }

        public void SetLastSceneIndex(int lastSceneIndex)
        {
            _lastSceneIndex = lastSceneIndex;
        }

        public int GetScore()
        {
            return _score;
        }

        public void SetScore(int score)
        {
            _score = score;
        }
    }
}

