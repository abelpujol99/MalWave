using UnityEngine;


namespace Event
{
    public class SceneChangerManager : MonoBehaviour
    {
        private static SceneChangerManager _instance;

        public static SceneChangerManager Instance
        {
            get { return _instance; }
        }

        [SerializeField] private int _lastSceneIndex;
        [SerializeField] private int _score;
        [SerializeField] private int _restartToLevel;
        [SerializeField] private bool _activateTutorial = true;

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

        public bool GetActivateTutorial()
        {
            return _activateTutorial;
        }

        public void SetActivateTutorial(bool activateTutorial)
        {
            _activateTutorial = activateTutorial;
        }

        public int GetRestartToLevel()
        {
            return _restartToLevel;
        }

        public void SetRestartToLevel(int restartToLevel)
        {
            _restartToLevel = restartToLevel;
        }
    }
}

