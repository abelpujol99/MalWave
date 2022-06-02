using System;
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
        [SerializeField] private GameObject _hand;

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
            InitializeAttacksDictionary();
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

        private void InitializeAttacksDictionary()
        {
            var dictionary = GetAttackDictionary();
            dictionary.Add(0, PortalBullets);
            dictionary.Add(1, DisperseBullets);
            //dictionary.Add(2, );
        }

        void Update()
        {
            base.Update();
            _currentHandOffset = new Vector3(transform.position.x - _initHandOffset.x, transform.position.y + _initHandOffset.y,
                transform.position.z + _initHandOffset.z);
            _hand.transform.position = _currentHandOffset;
        }

        private void PortalBullets()
        {
            
        }

        private void DisperseBullets()
        {
            for (int i = 0; i < _numLargeBullets; i++)
            {
                GameObject disperseBullet;
                disperseBullet = ReturnBullet(_largeBulletsQueue);
                BossBullet bossBullet = disperseBullet.GetComponent<BossBullet>();
                bossBullet.SpawnBullet(_largeBulletsDegrees[i], _currentHandOffset);
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
            DisperseBullets();
        }
    }
}