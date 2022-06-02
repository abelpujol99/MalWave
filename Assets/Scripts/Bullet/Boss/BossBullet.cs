using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Bullet.Boss
{
    public class BossBullet : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private Vector3 _direction;

        private Color _opacity;
        
        [SerializeField] private float _speed;

        private void Start()
        {
            _opacity = _spriteRenderer.color;
            _opacity.a = 0;
            //_spriteRenderer.color = _opacity;
        }

        private void Update()
        {
            if (_opacity.a < 1)
            {
                _opacity.a += 0.1f * Time.deltaTime;
            }
            else
            {
                ShootBullet();
            }
        }

        public void SpawnBullet(float angle, Vector3 offset)
        {
            
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            gameObject.transform.rotation = targetRotation;
                
            Vector3 vector = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            //_direction = vector;
            gameObject.transform.position = offset + 2f * vector;
            
            gameObject.SetActive(true);
        }

        public void ShootBullet()
        { 
            //_rigidbody2D.AddForce();
        }
    }
}