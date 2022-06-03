using Characters.Boss;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Bullet.Boss
{
    public class LargeYellowBossBullet : BossBullet
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Color _opacity;

        private Characters.Boss.Boss _boss;

        private void Start()
        {
            SetSpeed(200f);
            _boss = FindObjectOfType<YellowBoss>().GetComponent<YellowBoss>();
        }

        private void Update()
        {
            if (_opacity.a < 1)
            {
                _opacity.a += 0.5f * Time.deltaTime;
                _spriteRenderer.color = _opacity;
                transform.position = _boss.GetOffset() + 2f * GetDirection();
            }
            else
            {
                ShootBullet();
            }
        }

        public override void SpawnBullet(float angle)
        {
            SetAngle(angle);
            
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = targetRotation;
            GetRigidBody().velocity = new Vector2(0, 0);
                
            Vector3 vector = Quaternion.AngleAxis(GetAngle(), Vector3.forward) * Vector3.right;
            SetDirection(vector);
            
            ResetOpacity();

            gameObject.SetActive(true);
        }

        void ResetOpacity()
        {
            _opacity = _spriteRenderer.color;
            _opacity.a = 0;
            _spriteRenderer.color = _opacity;
        }

        protected override void ShootBullet()
        { 
            GetRigidBody().AddForce(GetDirection().normalized * GetSpeed());
        }
    }
}