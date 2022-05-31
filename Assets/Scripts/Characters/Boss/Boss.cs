using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Characters.Boss
{
    public abstract class Boss : MonoBehaviour
    {
        private Dictionary<int, Action> _attacksDictionary;

        [SerializeField] private GameObject _tinyBullet;
        [SerializeField] private GameObject _largeBullet;
        [SerializeField] private GameObject _fastBullet;

        [SerializeField] private UnityEngine.Camera _camera;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [SerializeField] private Material _flashMaterial;
        [SerializeField] private Material _originalMaterial;
        
        [SerializeField] private int _health;
        [SerializeField] private int _currentAttack;
        [SerializeField] private int _maxAttacks;

        [SerializeField] private float _timeToChangeAttack;
        [SerializeField] private float _currentTimeToChangeAttack = 0;
        [SerializeField] private float _durationOfFlash;
        
        // Start is called before the first frame update
        protected void Start()
        {
            _attacksDictionary = new Dictionary<int, Action>();
            transform.position = new Vector3(_camera.transform.position.x + 12.5f, _camera.transform.position.y - 0.3f,
                transform.position.z);
            _currentAttack = Random.Range(0, _maxAttacks);
        }

        // Update is called once per frame
        protected void Update()
        {
            if (transform.position.x >= 5)
            {
                Translate();
            }
            else
            {
                Move();    
            }

            Attack();

            Die();
        }

        void Translate()
        {
            transform.position = new Vector3(transform.position.x, _camera.transform.position.y - 0.3f,
                transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(
                _camera.transform.position.x + 5, _camera.transform.position.y - 0.3f,
                transform.position.z), 3 * Time.deltaTime);
        }

        void Move()
        {
            transform.position = new Vector3(_camera.transform.position.x + 5, _camera.transform.position.y - 0.3f,
                transform.position.z);
        }

        void Attack()
        {
            if (_timeToChangeAttack > _currentTimeToChangeAttack)
            {
                _currentTimeToChangeAttack += Time.deltaTime;
            }
            else
            {
                _currentTimeToChangeAttack = 0;
                _currentAttack = Random.Range(0, _maxAttacks);
            }
        }

        void Die()
        {
            if (_health <= 0)
            {
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

        protected Dictionary<int, Action> GetAttackDictionary()
        {
            return _attacksDictionary;
        }

        protected void SetAttackDictionary(Dictionary<int, Action> attackDictionary)
        {
            _attacksDictionary = attackDictionary;
        }

        private IEnumerator Flash()
        {
            _spriteRenderer.material = _flashMaterial;
            yield return new WaitForSeconds(_durationOfFlash);
            _spriteRenderer.material = _originalMaterial;
        }
    }
}

