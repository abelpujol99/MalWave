using System;
using Characters.Boss;
using Characters.Main;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Bullet.Boss
{
    public abstract class BossBullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private Characters.Boss.Boss _boss;

        private Player _player;
        
        private Vector3 _direction;
        
        private float _speed;
        private float _angle;

        private bool _shoot;

        protected void Start()
        {
            _boss = FindObjectOfType<Characters.Boss.Boss>().GetComponent<Characters.Boss.Boss>();
        }

        protected Rigidbody2D GetRigidBody()
        {
            return _rigidbody2D;
        }

        protected void SetSpeed(float speed)
        {
            _speed = speed;
        }

        protected float GetSpeed()
        {
            return _speed;
        }

        protected void SetAngle(float angle)
        {
            _angle = angle;
        }

        protected float GetAngle()
        {
            return _angle;
        }

        protected void SetDirection(Vector3 direction)
        {
            _direction = direction;
        }

        protected bool GetShoot()
        {
            return _shoot;
        }

        protected void SetShoot(bool shoot)
        {
            _shoot = shoot;
        }

        protected Vector3 GetDirection()
        {
            return _direction;
        }

        protected Characters.Boss.Boss GetBoss()
        {
            return _boss;
        }

        public virtual void SpawnBullet(float angle, Player player)
        {
            _player = player;
            
            SetAngle(angle);
            
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = targetRotation;
            GetRigidBody().velocity = new Vector2(0, 0);
                
            Vector3 vector = Quaternion.AngleAxis(GetAngle(), Vector3.forward) * Vector3.right;
            SetDirection(vector);

            SetShoot(false);

            gameObject.SetActive(true);
        }

        protected abstract void ShootBullet();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 7)
            {
                gameObject.SetActive(false);
                _player.Death(_boss.name.Split('(')[0]);
            }
        }
        
    }
}