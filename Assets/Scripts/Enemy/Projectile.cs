using System;
using UnityEngine;
using Characters.Main;

namespace Enemy
{
    public class Projectile : MonoBehaviour
    {
        public float speed;

        private GameObject _shooter;

        private Vector3 _enemyPosition;
        
        private Vector2 _target;
        
        private Player _player;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.gameObject.layer)
            {
                case 7:
                    if (_shooter != null)
                    {
                        _player.Death(_shooter.name.Split('(')[0]);
                    }
                    Destroy(gameObject);
                    break;
                case 6:
                    Destroy(gameObject);
                    break;
            }
        }

        public void Shoot(GameObject shooter, Player player)
        {
            _shooter = shooter;
            _player = player;
            _enemyPosition = _shooter.gameObject.transform.position;
            _target = new Vector2(_player.transform.position.x, _player.transform.position.y);
            
            double angle = Math.Atan2(_target.y - _enemyPosition.y, _target.x - _enemyPosition.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler((new Vector3(0, 0, (float)angle)));
            transform.rotation = targetRotation;
            
            transform.position = new Vector3(_target.x - _enemyPosition.x, _target.y - _enemyPosition.y, 0).normalized * 0.9f + _enemyPosition;
            _rigidbody2D.AddForce(new Vector2(_target.x - transform.position.x, _target.y - transform.position.y).normalized * speed, ForceMode2D.Impulse);
        } 
    }
}

