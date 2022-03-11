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

        private delegate void JumpAction();
        private static event JumpAction onJumpButton;

        [SerializeField] private float _speed;

        private Animator _animator;
        
        private Rigidbody2D _rb2D;

        private RaycastHit2D _checkGround;

        private Dictionary<string, EventManager.Action> _animationsTransitions;

        [SerializeField] private bool _ground;
        [SerializeField] private bool _canDoubleJump;
        
        [SerializeField] private float _moveSpeed = 1;
        [SerializeField] private float _jumpSpeed = 9;
        [SerializeField] private float _doubleJumpSpeed = 6;
        [SerializeField] private float _fallMultiplier = 0.5f;
        [SerializeField] private float _lowJumpMultiplier = 1f;
        
        void Start()
        {
            _animator = GetComponent<Animator>();
            _rb2D = GetComponent<Rigidbody2D>();
        }
        
        void Update()
        {
            Move();
            
            CheckJump();
            
            CheckFall();
        }

        private void FixedUpdate()
        {
            CheckLowHighJump();
        }

        void Move()
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _rb2D.velocity = new Vector2(_moveSpeed, _rb2D.velocity.y);
            } 
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _rb2D.velocity = new Vector2(-_moveSpeed, _rb2D.velocity.y);
            }
            else
            {
                _rb2D.velocity = new Vector2(0, _rb2D.velocity.y);
            }
        }

        void CheckJump()
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                if (_ground)
                {
                    _rb2D.velocity = new Vector2(_rb2D.velocity.x, _jumpSpeed);
                    _ground = false;
                    _animator.SetBool("Jump", true);
                    StartCoroutine(SetAnimationFalse("Jump", _animator.))
                }
                else if (_canDoubleJump && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
                {
                    _rb2D.velocity = new Vector2(_rb2D.velocity.x, _doubleJumpSpeed);
                    _canDoubleJump = false;
                }
            }
        }

        void CheckFall()
        {
            if (_rb2D.velocity.y < 0)
            {
                _animator.SetBool("Fall", true);
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

        private IEnumerator SetAnimationFalse(string state, float time)
        {
            yield return new WaitForSeconds(time);
            _animator.SetBool(state, false);
        }

        private void OnCollisionEnter2D(Collision2D collision2D)
        {
            if (Vector2.Angle(collision2D.contacts[0].normal, Vector2.up) <= 60f && collision2D.contacts[0].collider.gameObject.layer == 6)
            {
                if (_animator.GetBool("Fall"))
                {
                    _animator.SetBool("Fall", false);
                }
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

