using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Characters.Boss
{
    public class YellowBoss : Boss
    {
        [SerializeField] private Vector3 _handOffset;

        [SerializeField] private GameObject _portal;

        private Queue<GameObject> _fastBullets;
        private Queue<GameObject> _tinyBullets;

        [SerializeField] private int _numFastBullets;
        [SerializeField] private int _numTinyBullets;

        void Start()
        {
            base.Start();
            InitializeAttacksDictionary();
        }

        void InitializeAttacksDictionary()
        {
            var dictionary = GetAttackDictionary();
            dictionary.Add(0, PortalBullets);
            dictionary.Add(1, DisperseBullets);
            //dictionary.Add(2, );
        }

        void PortalBullets()
        {
            
        }

        void DisperseBullets()
        {
            
        }

        void Update()
        {
            base.Update();
            
            _handOffset = new Vector3(transform.position.x + _handOffset.x, transform.position.y + _handOffset.y,
                transform.position.z + _handOffset.z);
        }
    }
}