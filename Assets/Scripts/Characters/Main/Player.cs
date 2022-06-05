using System;
using System.Collections;
using System.Collections.Generic;
using Bullet;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Characters.Main
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
        private const String SURF_ANIMATOR_NAME = "Surf";

        [SerializeField] private Animator _animator;
        
        [SerializeField] private Rigidbody2D _rb2D;

        private RaycastHit2D _checkGround;

        private Queue<GameObject> _bulletQueue;

        [SerializeField] private GameObject _bullet;

        private Vector3 _dashTargetPosition; 

        private bool _jump;
        private bool _doubleJump;
        private bool _ground;
        private bool _canDoubleJump;
        private bool _dash;
        private bool _canDash;
        private bool _shoot;
        private bool _dead;
        private bool _surf;
        private bool _cooldownDashRestarting;
        private bool _win;

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

        private String _killer;

        private void Start()
        {
            CheckSurf();
            InitializeMag();
        }

        private void InitializeMag()
        {
            _bulletQueue = new Queue<GameObject>();

            for (int i = 0; i < _magSize; i++)
            {
                GameObject obj = Instantiate(_bullet);
                obj.transform.parent = transform;
                obj.SetActive(false);
                _bulletQueue.Enqueue(obj);
            }
        }

        private void Update()
        {
            if (!_surf)
            {
                CheckJumpAndDoubleJump();    

                CheckFall();

                CheckDash();
            }

            if (transform.position.y < -10)
            {
                Death("Gravity");
            }
        }

        public void Death(string killer)
        {
            _killer = killer;
            _dead = true;
            _animator.SetTrigger(DEATH_ANIMATOR_NAME);
            _rb2D.isKinematic = true;
            _rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            _rb2D.velocity = new Vector2(0, 0);
        }

        private void FixedUpdate()
        {
            if (!_dead)
            {
                RunOrDash();    
            }

            if (_surf)
            {
                TranslateYAxisWhileSurfing();   
            }
            else
            {
                Jump();

                DoubleJump();
            
                CheckLowHighJump();
            }
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
            if (_ground && Input.GetKeyDown(KeyCode.Space))
            {
                _jump = true;
            }
            else if (!_ground && _canDoubleJump && Input.GetKeyDown(KeyCode.Space))
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
            _checkGround = Physics2D.Raycast(transform.position, 
                Vector2.down, 1f, LayerMask.GetMask("Ground"));
            if (!_checkGround)
            {
                _ground = false;
                _animator.SetBool(FALL_ANIMATOR_NAME, true);
                _animator.SetBool(LAND_ANIMATOR_NAME, false);
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
                if (_dashTargetPosition.x < transform.position.x)
                {
                    EndDash();
                }
            }
        }

        private void EndDash()
        {
            _dash = false;
            _currentRunSpeed = 2.5f;
            _rb2D.velocity = Vector2.zero;
            _rb2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;

            if (_ground && !_cooldownDashRestarting)
            {
                StartCoroutine(ResetDashCooldown(_dashCooldown));   
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
                MainCharacterBullet mainCharacterBullet = bulletToSpawn.GetComponent<MainCharacterBullet>();
                mainCharacterBullet.ShootBullet(transform.position);
            }
        }

        private void CheckSurf()
        {
            if (SceneManager.GetActiveScene().name == "Tuto 0.2.2")
            {
                _surf = true;
                _animator.SetBool(SURF_ANIMATOR_NAME, true);
            }
        }

        private void TranslateYAxisWhileSurfing()
        {
            if (Input.GetKey(KeyCode.Space)) 
            { 
                _rb2D.velocity += Vector2.up * -(Physics.gravity.y) * Time.deltaTime; 
            } 
            else 
            { 
                _rb2D.velocity += Vector2.up * Physics.gravity.y * Time.deltaTime; 
            } 
        }

        private GameObject SpawnBullet()
        {
            GameObject bulletToSpawn;

            bulletToSpawn = _bulletQueue.Dequeue();
            _bulletQueue.Enqueue(bulletToSpawn);

            return bulletToSpawn;

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
            }else if (_dash && Vector2.Angle(collision.contacts[0].normal, Vector2.left) <= 60f)
            {
                EndDash();
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!_canDash && !_cooldownDashRestarting && collision.gameObject.name == "Tilemap")
            {
                StartCoroutine(ResetDashCooldown(_dashCooldown));
            }
        }

        public String GetKiller()
        {
            return _killer;
        }

        public void SetWin()
        {
            _win = true;
        }

        public bool GetWin()
        {
            return _win;
        }

        public bool ReturnDash()
        {
            return _dash;
        }

        public bool GetDeath()
        {
            return _dead;
        }

        public void StartDashing()
        {
            _rb2D.AddForce(new Vector2(_dashTargetPosition.x - transform.position.x, _dashTargetPosition.y - transform.position.y) * _dashSpeed, ForceMode2D.Impulse);
        }
    }
}

