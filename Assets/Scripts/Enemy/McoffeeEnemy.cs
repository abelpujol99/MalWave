using System;
using Characters.Main;
using UnityEngine;

namespace Enemy
{
    public class McoffeeEnemy : MonoBehaviour
    {
    
        [SerializeField] private Player _player;

        private Vector3 _targetPosition;

        private float _distanceToMoveOnYAxis = 0.3f;

        private void Start()
        {
            _targetPosition = new Vector3(transform.position.x, transform.position.y + _distanceToMoveOnYAxis,
                transform.position.z);
        }

        private void FixedUpdate()
        {
            ChangeTargetPosition();
        
            Move();
        }

        private void ChangeTargetPosition()
        {
            if (transform.position == _targetPosition)
            {
                _distanceToMoveOnYAxis *= -1;
                _targetPosition = new Vector3(transform.position.x, transform.position.y + _distanceToMoveOnYAxis,
                    transform.position.z);
            }
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Math.Abs(_distanceToMoveOnYAxis) * Time.deltaTime) ;
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 7 && _player.GetDash())
            {
                Destroy(gameObject);
            }
            else if (other.gameObject.layer == 7 && !_player.GetDash())
            {
                _player.Death(gameObject.name.Split('(')[0]);
            }
        }

    }
}

