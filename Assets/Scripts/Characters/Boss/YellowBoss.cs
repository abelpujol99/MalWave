using System;
using System.Collections;
using System.Collections.Generic;
using Bullet.Boss;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Characters.Boss
{
    public class YellowBoss : Boss
    {
        [SerializeField] private Vector3 _initHandOffset;
        [SerializeField] private Vector3 _currentHandOffset;
        private Vector3[] _vectorLargeBullets;

        [SerializeField] private GameObject _portal;
        [SerializeField] private GameObject _largeBullet;
        [SerializeField] private GameObject _tinyBullet;

        private Queue<GameObject> _largeBulletsQueue;
        private Queue<GameObject> _tinyBulletsQueue;

        private int _numLargeBullets;
        [SerializeField] private int _numTinyBullets;

        [SerializeField] private float[] _largeBulletsDegrees;

        void Start()
        {
            base.Start();
            _numLargeBullets = _largeBulletsDegrees.Length;
            InitializeBulletsQueues();
        }

        private void InitializeBulletsQueues()
        {
            _largeBulletsQueue = new Queue<GameObject>();
            _tinyBulletsQueue = new Queue<GameObject>();
            
            for (int i = 0; i < _numLargeBullets; i++)
            {
                GameObject obj = Instantiate(_largeBullet);
                obj.SetActive(false);
                _largeBulletsQueue.Enqueue(obj);
            }

            for (int i = 0; i < _numTinyBullets; i++)
            {
                GameObject obj = Instantiate(_tinyBullet);
                obj.SetActive(false);
                _tinyBulletsQueue.Enqueue(obj);
            }
        }

        void Update()
        {
            base.Update();
            _currentHandOffset = new Vector3(transform.position.x - _initHandOffset.x, transform.position.y + _initHandOffset.y,
                transform.position.z + _initHandOffset.z);
        }

        private void Bullets(int numBullets, Queue<GameObject> bulletsQueue)
        {
            for (int i = 0; i < numBullets; i++)
            {
                GameObject bullet;
                bullet = ReturnBullet(bulletsQueue);
                BossBullet bossBullet = bullet.GetComponent<BossBullet>();
                StartCoroutine(DelayBullets(1, i, bossBullet));
            }
        }

        private IEnumerator DelayBullets(float time, int i, BossBullet bullet)
        {
            yield return new WaitForSeconds(time * i);
            if (bullet.GetComponent<LargeYellowBossBullet>() != null)
            {
                bullet.SpawnBullet(_largeBulletsDegrees[i]);
            }
            else
            {
                bullet.SpawnBullet(180);
            }
        }

        private GameObject ReturnBullet(Queue<GameObject> bulletsQueue)
        {
            GameObject bulletToSpawn;
            bulletToSpawn = bulletsQueue.Dequeue();
            bulletsQueue.Enqueue(bulletToSpawn);
            return bulletToSpawn;
        }

        protected override void FirstAttack()
        {
            Bullets(_numLargeBullets, _largeBulletsQueue);
        }

        protected override void SecondAttack()
        {
            Bullets(_numTinyBullets, _tinyBulletsQueue);
        }

        public override Vector3 GetOffset()
        {
            return _currentHandOffset;
        }
    }
}