using System;
using UnityEngine;
using Characters.Main;

namespace Enemy
{
    public class Projectile : MonoBehaviour
    {
        public float speed;
        
        private Vector2 _target;
        private Vector3 _enemyPosition;
        
        private Player _player;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.layer == 7)
            {
                Destroy(gameObject);
                _player.Death(gameObject.name.Split('(')[0]);
            }
        }

        public void whoShoot(Vector3 enemyPosition, Player player)
        {
            _player = player;
            _enemyPosition = enemyPosition;
            _target = new Vector2(_player.transform.position.x, _player.transform.position.y);
            
            double angle = Math.Atan2(_target.y - _enemyPosition.y, _target.x - _enemyPosition.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler((new Vector3(0, 0, (float)angle)));
            transform.rotation = targetRotation;
            
            transform.position = new Vector3(_target.x - _enemyPosition.x, _target.y - _enemyPosition.y, 0).normalized * 0.5f + _enemyPosition;
            _rigidbody2D.AddForce(new Vector2(_target.x - transform.position.x, _target.y - transform.position.y).normalized * speed, ForceMode2D.Impulse);
        } 
    }
}

