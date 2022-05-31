using Characters.Main;
using Event;
using TMPro;
using UnityEngine;

namespace Event
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Player _player;
        private int _score;

        private void Update()
        {
            if (_player.GetDeath())
            {
                _panel.SetActive(false);
            }
            else
            {
                _score = SceneChangerManager.Instance.GetScore();
                _scoreText.SetText("SCORE: " + _score);    
            }
        }

    }
}


