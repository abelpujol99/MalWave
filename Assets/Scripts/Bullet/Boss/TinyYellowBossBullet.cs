using System.Collections;
using System.Collections.Generic;
using Bullet.Boss;
using Characters.Boss;
using UnityEngine;

namespace Bullet.Boss
{
    public class TinyYellowBossBullet : BossBullet
    {

        [SerializeField] private Sprite _portal;

        private GameObject _boss;

        private float _bossXOffset;
        private float _YOffset;
        private float _ceil = 1.35f;
        private float _floor = -4f;

        // Start is called before the first frame update
        void Start()
        {
            _boss = FindObjectOfType<YellowBoss>().gameObject;
            SetSpeed(100f);
        }

        // Update is called once per frame
        void Update()
        {
            _bossXOffset = _boss.transform.position.x;
            transform.position = new Vector3(_bossXOffset - 4, _YOffset, 0);
        }

        public override void SpawnBullet(float angle)
        {
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, 180));
            transform.rotation = targetRotation;
                
            Vector3 vector = Quaternion.AngleAxis(GetAngle(), Vector3.forward) * Vector3.right;
            SetDirection(vector);

            _YOffset = Random.Range(_ceil, _floor);

            gameObject.SetActive(true);

        }

        protected override void ShootBullet()
        {
            GetRigidBody().AddForce(GetDirection().normalized * GetSpeed());
        }

    }
}

