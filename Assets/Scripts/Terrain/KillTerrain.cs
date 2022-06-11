using Characters.Main;
using UnityEngine;


namespace Terrain
{
    public class KillTerrain : MonoBehaviour
    {
        [SerializeField] private Player _player; 
    
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 7)
            {
                _player.Death("FireWall");
            }
        }
    }
}

