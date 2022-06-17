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

        private int _lastSceneIndex;
        private int _score;
        private int _restartToLevel;
        private bool _activateTutorial = true;
        private bool _completeTutorial;
        private bool _completeLevel1;
        
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

        public bool GetCompleteTutorial()
        {
            return _completeTutorial;
        }

        public void SetCompleteTutorial()
        {
            _completeTutorial = true;
        }

        public bool GetCompleteLevel1()
        {
            return _completeLevel1;
        }

        public void SetCompleteLevel1()
        {
            _completeLevel1 = true;
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

