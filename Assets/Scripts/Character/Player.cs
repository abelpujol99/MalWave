using System;
using System.Collections;
using System.Collections.Generic;
using Event;
using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    public class Player : MonoBehaviour
    {
        private const String JUMP_ANIMATOR_NAME = "Jump";
        private const String DOUBLEJUMP_ANIMATOR_NAME = "Double Jump";
        private const String FALL_ANIMATOR_NAME = "Fall";

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
        
        [SerializeField] private float _moveSpeed = 1;
        [SerializeField] private float _jumpSpeed = 9;
        [SerializeField] private float _doubleJumpSpeed = 6;
        [SerializeField] private float _fallMultiplier = 0.5f;
        [SerializeField] private float _lowJumpMultiplier = 1f;


        [SerializeField] private float _horizontalMove;

        //DASH
        [SerializeField] private float _dashDistance = 15f;
        bool isDashing = false;
        float doubleTapTime;
        KeyCode lastKeyCode;

        void Start()
        {
            _horizontalMove = 0f;
        }
        
        void Update()
        {
            CheckMove();
            
            CheckJumpAndDoubleJump();

            CheckFall();

            //DASH
            if(Input.GetKeyDown(KeyCode.D))
            {
                if(doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
                {
                    StartCoroutine(Dash(1f));
                }
                else
                {
                    doubleTapTime = Time.time + 0.5f;
                }
                lastKeyCode = KeyCode.D;
            }
        }

        private void FixedUpdate()
        {
            Move();

            Jump();

            DoubleJump();
            
            CheckLowHighJump();

            //DASH
            /*if (!isDashing)
                _rb2D.velocity = new Vector2(mx*speed, _rb2D.velocity.y);*/

        }
        //DASH
        IEnumerator Dash (float direction)
        {
            isDashing = true;
            _rb2D.velocity = new Vector2(_rb2D.velocity.x, 0f);
            _rb2D.AddForce(new Vector2(_dashDistance * direction, 0f), ForceMode2D.Impulse);
            float gravity = _rb2D.gravityScale;
            _rb2D.gravityScale = 0;
            yield return new WaitForSeconds(0.4f);
            isDashing = false;
            _rb2D.gravityScale = gravity;
        }
        void CheckMove()
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        }
        
        void Move()
        {
            _rb2D.velocity = new Vector2(_horizontalMove, _rb2D.velocity.y);
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
                _animator.SetBool(DOUBLEJUMP_ANIMATOR_NAME, true);
                StartCoroutine(SetAnimationFalse(DOUBLEJUMP_ANIMATOR_NAME, ReturnAnimationClip(DOUBLEJUMP_ANIMATOR_NAME).length));
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
        
        AnimationClip ReturnAnimationClip(string name)
        {
            for (int i = 0; i < _animator.runtimeAnimatorController.animationClips.Length; i++)
            {
                if (_animator.runtimeAnimatorController.animationClips[i].name == JUMP_ANIMATOR_NAME)
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

        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            if (Vector2.Angle(collision2D.contacts[0].normal, Vector2.up) <= 60f && collision2D.contacts[0].collider.gameObject.layer == 6)
            {
                _animator.SetBool(FALL_ANIMATOR_NAME, false);
                _ground = true;
                _canDoubleJump = true;
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

