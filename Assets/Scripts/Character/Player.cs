using System;
using System.Collections;
using System.Collections.Generic;
using Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Character
{
    public class Player : MonoBehaviour
    {
        private const String JUMP_ANIMATOR_NAME = "Jump";
        private const String DOUBLEJUMP_ANIMATOR_NAME = "Double Jump";
        private const String FALL_ANIMATOR_NAME = "Fall";
        private const String LAND_ANIMATOR_NAME = "Land";
        private const String DASH_ANIMATOR_NAME = "Dash";

        private delegate void JumpAction();
        private static event JumpAction onJumpButton;
        
        

        [SerializeField] private Animator _animator;
        
        [SerializeField] private Rigidbody2D _rb2D;

        private RaycastHit2D _checkGround;

        private Dictionary<string, EventManager.Action> _animationsTransitions;

        [SerializeField] private bool _jump;
        [SerializeField] private bool _doubleJump;
        [SerializeField] private bool _ground;
        [SerializeField] private bool _canDoubleJump;
        [SerializeField] private bool _dash;
        [SerializeField] private bool _canDash;
        private bool _cooldownDashRestarting;

        [SerializeField] private float _moveSpeed = 2;
        [SerializeField] private float _jumpSpeed = 9;
        [SerializeField] private float _doubleJumpSpeed = 6;
        [SerializeField] private float _fallMultiplier = 0.5f;
        [SerializeField] private float _lowJumpMultiplier = 1f;
        [SerializeField] private float _dashCooldown = 1f;
        [SerializeField] private float _dashSpeed = 5f;
        
        void Update()
        {
            CheckJumpAndDoubleJump();

            CheckFall();

            CheckDash();

            if (transform.position.y < -4)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        private void FixedUpdate()
        {
            Move();

            Jump();

            DoubleJump();
            
            CheckLowHighJump();

            Dash();
        }
        
        void Move()
        {
            _rb2D.velocity = new Vector2(_moveSpeed, _rb2D.velocity.y);
        }

        void CheckJumpAndDoubleJump()
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

        void Jump()
        {
            if (_jump)
            {
                _rb2D.velocity = new Vector2(_rb2D.velocity.x, _jumpSpeed);
                _jump = false;
                _ground = false;
                _animator.SetBool(JUMP_ANIMATOR_NAME, true);
                _animator.SetBool(LAND_ANIMATOR_NAME, false);
                StartCoroutine(SetAnimationFalse(JUMP_ANIMATOR_NAME, ReturnAnimationClip(JUMP_ANIMATOR_NAME).length));
            }
        }

        void DoubleJump()
        {
            if (_doubleJump && _canDoubleJump)
            {
                _rb2D.velocity = new Vector2(_rb2D.velocity.x, _doubleJumpSpeed);    
                _doubleJump = false;
                _canDoubleJump = false;
                _animator.SetBool(FALL_ANIMATOR_NAME, false);
                _animator.SetBool(DOUBLEJUMP_ANIMATOR_NAME, true);
                StartCoroutine(SetAnimationFalse(DOUBLEJUMP_ANIMATOR_NAME, ReturnAnimationClip(JUMP_ANIMATOR_NAME).length));
            }
        }

        void CheckFall()
        {
            if (_rb2D.velocity.y < 0 && !_ground)
            {
                _animator.SetBool(FALL_ANIMATOR_NAME, true);
            }
        }

        void CheckLowHighJump()
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

        void CheckDash()
        {
            if (Input.GetKeyDown(KeyCode.E) && _canDash)
            {
                _dash = true;
                _canDash= false;
            }
        }

        void Dash()
        {
            if (_dash)
            {
                _rb2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                _rb2D.AddForce(new Vector2(5, _rb2D.velocity.y));
                _dash = false;
                StartCoroutine(SetAnimationFalse(DASH_ANIMATOR_NAME, ReturnAnimationClip(DASH_ANIMATOR_NAME).length));
                StartCoroutine(UnfreezePositionY(ReturnAnimationClip(DASH_ANIMATOR_NAME).length));
            }
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

        private IEnumerator UnfreezePositionY(float time)
        {
            yield return new WaitForSeconds(time);
            _rb2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }

        private IEnumerator ResetDashCooldown(float time)
        {
            _cooldownDashRestarting = true;
            yield return new WaitForSeconds(time);
            _canDash = true;
            _cooldownDashRestarting = false;
        }

        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            if (Vector2.Angle(collision2D.contacts[0].normal, Vector2.up) <= 60f && collision2D.contacts[0].collider.gameObject.layer == 6)
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

        private void OnCollisionExit2D(Collision2D collision2D)
        {
            if (collision2D.collider.gameObject.layer == 6)
            {
                _ground = false;
            }
        }
    }
}

