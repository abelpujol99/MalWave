using Characters.Main;
using UnityEngine;


namespace Terrain
{
    public class WinGround : MonoBehaviour
    {
        [SerializeField] private Player _player;
    
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 7)
            {
                _player.SetWin();
            }
        }
    }
}

