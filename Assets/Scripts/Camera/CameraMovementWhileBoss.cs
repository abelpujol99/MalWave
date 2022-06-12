using System;
using Characters.Main;
using UnityEngine;

namespace Camera
{
    public class CameraMovementWhileBoss : MonoBehaviour
    {
        [SerializeField] private Transform _characterPosition;

        [SerializeField] private Player _player;

        [SerializeField] private GameObject _winPanel;

        private float _cameraPositionY;

        private void Start()
        {
            _cameraPositionY = _characterPosition.position.y;
        }

        private void LateUpdate()
        {
            if (!_player.GetWin())
            {
                transform.position = new Vector3(_characterPosition.position.x + 6f, _cameraPositionY + 3f,
                    transform.position.z);
            }
            else
            {
                transform.position = _winPanel.transform.position;
            }
        }
    }
}