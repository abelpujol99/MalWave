using Characters.Main;
using Event;
using UnityEngine;

namespace Score
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private Player _player;

        [SerializeField] private int _score;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 7)
            {
                SceneChangerManager.Instance.SetScore(SceneChangerManager.Instance.GetScore() + _score);
                Destroy(gameObject);
            }
        }
    }
}

