using Characters.Main;
using Event;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Terrain
{
    public class WinGround : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 7)
            {
                if (SceneManager.GetActiveScene().buildIndex == 7)
                {
                    SceneChangerManager.Instance.SetCompleteTutorial();
                }
                else if (SceneManager.GetActiveScene().buildIndex == 14)
                {
                    SceneChangerManager.Instance.SetCompleteLevel1();
                }
                _player.SetWin();
            }
        }
    }
}

