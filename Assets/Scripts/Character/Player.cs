using System;
using System.Collections;
using System.Collections.Generic;
using Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Character
{
    public class Player : MonoBehaviour
    {
        private const String JUMP_ANIMATOR_NAME = "Jump";
        private const String DOUBLEJUMP_ANIMATOR_NAME = "Double Jump";
        private const String FALL_ANIMATOR_NAME = "Fall";
        private const String LAND_ANIMATOR_NAME = "Land";
        private const String DASH_ANIMATOR_NAME = "Dash";
        private const String SHOOT_ANIMATOR_NAME = "Shoot";
        private const String DEATH_ANIMATOR_NAME = "Death";

        [SerializeField] private Animator _animator;
        
        [SerializeField] private Rigidbody2D _rb2D;

        private RaycastHit2D _checkGround;

        private Queue<GameObject> _bulletQueue;

        [SerializeField] private GameObject _bullet;

        private Vector3 _dashTargetPosition; 

        [SerializeField] private bool _jump;
        [SerializeField] private bool _doubleJump;
        [SerializeField] private bool _ground;
        [SerializeField] private bool _canDoubleJump;
        [SerializeField] private bool _dash;
        [SerializeField] private bool _canDash;
        [SerializeField] private bool _shoot;
        [SerializeField] private bool _dead;
        private bool _cooldownDashRestarting;

        [SerializeField] private int _magSize = 5;
        
        [SerializeField] private float _currentRunSpeed;
        [SerializeField] private float _runSpeed = 2;
        [SerializeField] private float _jumpSpeed = 9;
        [SerializeField] private float _doubleJumpSpeed = 6;
        [SerializeField] private float _fallMultiplier = 0.5f;
        [SerializeField] private float _lowJumpMultiplier = 1f;
        [SerializeField] private float _dashCooldown = 1f;
        [SerializeField] private float _dashSpeed = 2f;
        [SerializeField] private float _dashDistance = 5f;
        [SerializeField] private float _currentShootCooldown;
        [SerializeField] private float _shootCooldown = 0.7f;

        private void Start()
        {
            InitializeMag();
        }

        private void InitializeMag()
        {
            _bulletQueue = new Queue<GameObject>();

            for (int i = 0; i < _magSize; i++)
            {
                GameObject obj = Instantiate(_bullet);
                obj.SetActive(false);
                _bulletQueue.Enqueue(obj);
            }
        }

        private void Update()
        {
            CheckJumpAndDoubleJump();

            CheckFall();

            CheckDash();

            if (transform.position.x > 1)
            {
                Death();
            }
        }

        private void Death()
        {
            _dead = true;
            _animator.SetTrigger(DEATH_ANIMATOR_NAME);
            _rb2D.isKinematic = true;
            _rb2D.velocity = new Vector2(0, 0);
        }

        private void FixedUpdate()
        {
            if (!_dead)
            {
                RunOrDash();    
            }

            Jump();

            DoubleJump();
            
            CheckLowHighJump();
        }

        private void RunOrDash()
        {
            if (_dash)
            {
                Dash();
            }
            else
            {
                CheckShoot();
                Shoot();
                Run();
            }
        }

        private void Run()
        {
            if (_currentRunSpeed < _runSpeed)
            {
                _currentRunSpeed += Time.deltaTime;
            }
            _rb2D.velocity = new Vector2(_currentRunSpeed, _rb2D.velocity.y);
        }

        private void CheckJumpAndDoubleJump()
        {
            if (_ground && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
            {
                _jump = true;
            }
            else if (!_ground && _canDoubleJump && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
            {
                _doubleJump = true;
            }
        }

        private void Jump()
        {
            if (_jump)
            {
                _rb2D.velocity = new Vector2(_rb2D.velocity.x, _jumpSpeed);
                _jump = false;
                _ground = false;
                _animator.SetTrigger(JUMP_ANIMATOR_NAME);
                _animator.SetBool(LAND_ANIMATOR_NAME, false);
            }
        }

        private void DoubleJump()
        {
            if (_doubleJump && _canDoubleJump)
            {
                _rb2D.velocity = new Vector2(_rb2D.velocity.x, _doubleJumpSpeed);    
                _doubleJump = false;
                _canDoubleJump = false;
                _animator.SetBool(FALL_ANIMATOR_NAME, false);
                _animator.SetTrigger(DOUBLEJUMP_ANIMATOR_NAME);
            }
        }
        
        private void CheckLowHighJump()
        {
            if (_ground)
            {
                _rb2D.velocity += Vector2.up * Physics.gravity * 0;
            }
            else if (_rb2D.velocity.y < 0)
            {
                _rb2D.velocity += Vector2.up * Physics.gravity.y * _fallMultiplier * Time.deltaTime;
            }
            else if (_rb2D.velocity.y > 0 && !(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
            {
                _rb2D.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier) * Time.deltaTime;
            }
        }

        private void CheckFall()
        {
            if (_rb2D.velocity.y < 0 && !_ground)
            {
                _animator.SetBool(FALL_ANIMATOR_NAME, true);
            }
        }

        private void CheckDash()
        {
            if (Input.GetKeyDown(KeyCode.E) && _canDash)
            {
                _dash = true;
                _canDash= false;
                _dashTargetPosition = new Vector3(transform.position.x + _dashDistance, transform.position.y, 0);
                _animator.SetTrigger(DASH_ANIMATOR_NAME);
                StartCoroutine(SetAnimationFalse(DASH_ANIMATOR_NAME, ReturnAnimationClip(DASH_ANIMATOR_NAME).length));
            }
        }

        private void Dash()
        {
            if (_dash)
            {
                _rb2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                if (Vector3.Distance( transform.position, _dashTargetPosition) < 0.1f)
                {
                    _dash = false;
                    _currentRunSpeed = 0.25f;
                    _rb2D.velocity = Vector2.zero;
                    _rb2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

                    if (_ground && !_cooldownDashRestarting)
                    {
                        StartCoroutine(ResetDashCooldown(_dashCooldown));   
                    }
                }
            }
        }

        private void CheckShoot()
        {
            if (Input.GetMouseButton(0))
            {
                _shoot = true;
            }
            else
            {
                _shoot = false;
            }
            _animator.SetBool(SHOOT_ANIMATOR_NAME, _shoot);
        }

        private void Shoot()
        {
            if (_currentShootCooldown > 0)
            {
                _currentShootCooldown -= Time.deltaTime;
            }

            if (_shoot && _currentShootCooldown <= 0)
            {
                _currentShootCooldown = _shootCooldown;
                GameObject bulletToSpawn;
                bulletToSpawn = SpawnBullet();
                Bullet.Bullet bullet = bulletToSpawn.GetComponent<Bullet.Bullet>();
                bullet.ShootBullet(transform.position);
            }
        }

        private GameObject SpawnBullet()
        {
            GameObject bulletToSpawn;

            bulletToSpawn = _bulletQueue.Dequeue();
            _bulletQueue.Enqueue(bulletToSpawn);

            return bulletToSpawn;

        }

        public bool ReturnDash()
        {
            return _dash;
        }

        AnimationClip ReturnAnimationClip(string name)
        {
            for (int i = 0; i < _animator.runtimeAnimatorController.animationClips.Length; i++)
            {
                if (_animator.runtimeAnimatorController.animationClips[i].name == name)
                {
                    return _animator.runtimeAnimatorController.animationClips[i];
                }
            }
            return null;
        }

        public bool GetDeah()
        {
            return _dead;
        }

        private IEnumerator SetAnimationFalse(string state, float time)
        {
            yield return new WaitForSeconds(time);
            _animator.SetBool(state, false);
        }

        private IEnumerator ResetDashCooldown(float time)
        {
            _cooldownDashRestarting = true;
            yield return new WaitForSeconds(time);
            _canDash = true;
            _cooldownDashRestarting = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (Vector2.Angle(collision.contacts[0].normal, Vector2.up) <= 60f && collision.contacts[0].collider.gameObject.layer == 6)
            {
                _animator.SetBool(FALL_ANIMATOR_NAME, false);
                _animator.SetBool(LAND_ANIMATOR_NAME, true);
                _ground = true;
                _canDoubleJump = true;
                if (!_canDash && !_cooldownDashRestarting)
                {
                    StartCoroutine(ResetDashCooldown(_dashCooldown));   
                }
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!_canDash && !_cooldownDashRestarting && collision.gameObject.name == "Tilemap")
            {
                StartCoroutine(ResetDashCooldown(_dashCooldown));
            }
        }

        void StartDashing()
        {
            _rb2D.AddForce(new Vector2(_dashTargetPosition.x - transform.position.x, _dashTargetPosition.y - transform.position.y) * _dashSpeed, ForceMode2D.Impulse);
        }
    }
}

