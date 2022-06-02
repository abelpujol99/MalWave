using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Bullet.Boss
{
    public class BossBullet : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private Vector3 _direction;
        private Vector3 _offset;

        private Color _opacity;
        
        [SerializeField] private float _speed;
        private float _angle;

        private void Start()
        {
            _speed = 200f;
        }

        private void Update()
        {
            if (_opacity.a < 1)
            {
                _opacity.a += 0.3f * Time.deltaTime;
                _spriteRenderer.color = _opacity;    
                
                Vector3 vector = Quaternion.AngleAxis(_angle, Vector3.forward) * Vector3.right;
                _direction = vector;
                transform.position = _offset + 2f * vector;
            }
            else
            {
                ShootBullet();
            }
        }

        public void SpawnBullet(float angle, Vector3 offset)
        {
            _offset = offset;
            _angle = angle;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            gameObject.transform.rotation = targetRotation;

            _rigidbody2D.velocity = new Vector2(0, 0);

            ResetOpacity();
            
            gameObject.SetActive(true);
            
        }

        void ResetOpacity()
        {
            _opacity = _spriteRenderer.color;
            _opacity.a = 0;
            _spriteRenderer.color = _opacity;
        }

        private void ShootBullet()
        { 
            _rigidbody2D.AddForce(_direction.normalized * _speed);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            gameObject.SetActive(false);
        }
    }
}