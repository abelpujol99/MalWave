using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Main;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Characters.Boss
{
    public abstract class Boss : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private Material _flashMaterial;
        [SerializeField] private Material _originalMaterial;

        [SerializeField] private Player _player;
        
        private int _currentAttack;
        private int _health;
        private int _maxAttacks;

        private float[] _timeToChangeAttackForEachAttak;
        private float[] _bulletsDelay;
        private float _currentTimeToChangeAttack;
        private float _durationOfFlash;
        private float _travelDistance = 3f;

        private bool _canAttack;

        protected void Start()
        {
            _health = 10;
            _maxAttacks = 2;
            _durationOfFlash = 0.125f;
            _timeToChangeAttackForEachAttak = new float[_maxAttacks];
            _bulletsDelay = new float[_maxAttacks];
            transform.position = new Vector3(_camera.transform.position.x + 12.5f, _camera.transform.position.y - 0.3f,
                transform.position.z);
            StartCoroutine(StartAttacking());
        }

        void Translate()
        {
            transform.position = new Vector3(transform.position.x, _camera.transform.position.y - 0.3f,
                transform.position.z);
            
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(
                _camera.transform.position.x + 5, _camera.transform.position.y - 0.3f,
                transform.position.z), _travelDistance * Time.deltaTime);

            if (transform.position.x < _camera.transform.position.x + 5)
            {
                _travelDistance = 8;
            }
        }

        protected void Update()
        {
            Translate();    

            if (CheckAttack())    
            {
                Attack();    
            }

            Die();
        }

        bool CheckAttack()
        {
            _currentTimeToChangeAttack += Time.deltaTime;

            return _canAttack = _currentTimeToChangeAttack >= _timeToChangeAttackForEachAttak[_currentAttack];
        }

        void Attack()
        {
            _currentTimeToChangeAttack = 0;
            _currentAttack = Random.Range(0, _maxAttacks);
            
            switch (_currentAttack)
            {
                case 0:
                    FirstAttack();
                    break;
                case 1:
                    SecondAttack();
                    break;
            }

            _canAttack = false;
        }

        void Die()
        {
            if (_health <= 0)
            {
                _player.SetWin();
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 8)
            {
                StartCoroutine(Flash());
                collision.gameObject.SetActive(false);
                _health--;
            }
        }

        protected Player GetPlayer()
        {
            return _player;
        }

        protected void SetTimeToChangeAttackForEachAttackArray(float[] times)
        {
            for (int i = 0; i < _timeToChangeAttackForEachAttak.Length; i++)
            {
                _timeToChangeAttackForEachAttak[i] = times[i];
            }
        }

        protected float[] GetTimeToChangeAttackForEachAttackArray()
        {
            return _timeToChangeAttackForEachAttak;
        }

        protected void SetBulletsDelayArray(float[] delays)
        {
            for (int i = 0; i < _bulletsDelay.Length; i++)
            {
                _bulletsDelay[i] = delays[i];
            }
        }

        protected float[] GetBulletsDelayArray()
        {
            return _bulletsDelay;
        }

        public abstract Vector3 GetHandOffset();
        protected abstract void FirstAttack();
        protected abstract void SecondAttack();

        private IEnumerator StartAttacking()
        {
            yield return new WaitForSeconds(2);
            _currentTimeToChangeAttack = _timeToChangeAttackForEachAttak[_currentAttack];
        }

        private IEnumerator Flash()
        {
            _spriteRenderer.material = _flashMaterial;
            yield return new WaitForSeconds(_durationOfFlash);
            _spriteRenderer.material = _originalMaterial;
        }
    }
}

