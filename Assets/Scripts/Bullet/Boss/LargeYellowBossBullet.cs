using Characters.Boss;
using Characters.Main;
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

        private void Start()
        {
            base.Start();
            SetSpeed(1600f);
        }

        private void Update()
        {
            if (_opacity.a < 1)
            {
                _opacity.a += 0.5f * Time.deltaTime;
                _spriteRenderer.color = _opacity;
                transform.position = GetBoss().GetHandOffset() + 2f * GetDirection();
            }
            else if(!GetShoot())
            {
                ShootBullet();
            }
        }

        public override void SpawnBullet(float angle, Player player)
        {
            base.SpawnBullet(angle, player);

            ResetOpacity();
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
            SetShoot(true);
        }
    }
}