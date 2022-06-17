using System;
using System.Collections;
using System.Collections.Generic;
using Bullet.Boss;
using Characters.Boss;
using Characters.Main;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bullet.Boss
{
    public class TinyYellowBossBullet : BossBullet
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private Transform _portalTransform;
        
        private readonly Vector3 _scaleChange = new Vector3(0, 0.001f, 0);

        private float[] _portalsPositionY;
        
        private float _offsetY;
        private float _floor = -1.7f;
        private float _ceil;
        private float _distanceBetweenFloorAndCeil = 4.84f;
        private float _timeToShootWhenPortalCompletelyOpen = 0.5f;
        private float _distanceBetweenPortals = 0.605f;
        private float _portalOffsetX = 1.2f;
        private float _maxScale = 0.02f;

        private bool _portalDisappear;

        private String _portalName = "Yellow Boss Portal";

        private void Awake()
        {
            _ceil = _floor + _distanceBetweenFloorAndCeil;
            _portalsPositionY = new float[Mathf.FloorToInt((_ceil - _floor) / (_distanceBetweenPortals * 2))];
            for (int i = 0; i < _portalsPositionY.Length; i++)
            {
                _portalsPositionY[i] = _floor + i * _distanceBetweenPortals * 2;
            }
        }

        void Start()
        {
            base.Start();
            SetSpeed(200f);
        }

        void Update()
        {
            if (_portalTransform.localScale.y < _maxScale && !GetShoot())
            {
                transform.position = new Vector3(GetBoss().GetHandOffset().x - _portalOffsetX, GetBoss().GetHandOffset().y + _offsetY, 0);
                _portalTransform.localScale += _scaleChange;
            }
            else if (!GetShoot())
            {
                SetShoot(true);
                StartCoroutine(DelayBullet());
            }
            else if(_portalTransform.localScale.y >= 0f && _portalDisappear)
            {
                _portalTransform.localScale -= _scaleChange;
            }
            _portalTransform.position = new Vector3(GetBoss().GetHandOffset().x - _portalOffsetX, GetBoss().GetHandOffset().y + _offsetY, 0);
        }

        public override void SpawnBullet(float angle, Player player)
        {
            base.SpawnBullet(angle, player);
            
            _portalDisappear = false;
            _spriteRenderer.enabled = false;
            var localScale = _portalTransform.localScale;
            _portalTransform.transform.localScale = new Vector3(localScale.x, 0, localScale.z);
            _offsetY = _portalsPositionY[Random.Range(0, _portalsPositionY.Length)];

        }

        protected override void ShootBullet()
        {
            GetRigidBody().AddForce(GetDirection().normalized * GetSpeed());
        }

        private IEnumerator DelayBullet()
        {
            if (GetShoot())
            {
                yield return new WaitForSeconds(_timeToShootWhenPortalCompletelyOpen);
                _portalDisappear = true;
                _spriteRenderer.enabled = true;
                transform.position = new Vector3(GetBoss().GetHandOffset().x - _portalOffsetX, GetBoss().GetHandOffset().y + _offsetY, 0);
                ShootBullet();
            }
        }
    }
}

