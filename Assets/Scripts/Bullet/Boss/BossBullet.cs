using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Bullet.Boss
{
    public abstract class BossBullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private Vector3 _direction;
        
        private float _speed;
        private float _angle;
        
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

        protected Vector3 GetDirection()
        {
            return _direction;
        }

        public abstract void SpawnBullet(float angle);

        protected abstract void ShootBullet();

        private void OnTriggerEnter2D(Collider2D col)
        {
            gameObject.SetActive(false);
        }
        
    }
}