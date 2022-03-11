using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Camera
{
    public class CameraMovement : MonoBehaviour
    {
        private Transform _cameraPosition;
        [SerializeField] private Transform _characterPosition;

        private float _cameraPositionX;
        private float _cameraPositionY;
        
        void Start()
        {
            _cameraPosition = GetComponent<Transform>();
            _cameraPositionX = _cameraPosition.position.x;
            _cameraPositionY = _cameraPosition.position.y;
        }

        void Update()
        {
            _cameraPositionX = _characterPosition.position.x + 2f;
            _cameraPositionY = _characterPosition.position.y;
            transform.position = new Vector3(_cameraPositionX, _cameraPositionY, _cameraPosition.position.z);
        }
    }
}


