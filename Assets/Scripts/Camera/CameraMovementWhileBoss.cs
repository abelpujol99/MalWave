using System;
using Characters.Main;
using UnityEngine;

namespace Camera
{
    public class CameraMovementWhileBoss : MonoBehaviour
    {
        private Transform _cameraPosition;
        [SerializeField] private Transform _characterPosition;

        [SerializeField] private Player _player;

        private float _cameraPositionX;
        private float _cameraPositionY;

        private void LateUpdate()
        {
            if (!_player.GetWin())
            {
                transform.position = new Vector3(_characterPosition.position.x + 3f, _characterPosition.position.y,
                    transform.position.z);
            }
        }
    }
}