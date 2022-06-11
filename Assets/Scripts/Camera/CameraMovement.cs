using Characters.Main;
using UnityEngine;

namespace Camera
{
    public class CameraMovement : MonoBehaviour
    {
        private Transform _cameraPosition;
        [SerializeField] private Transform _characterPosition;

        [SerializeField] private Player _player;

        private Vector3 _targetPosition;
        private Vector3 _smoothedPosition;

        private float _cameraPositionX;
        private float _cameraPositionY;
        private float _smoothSpeed = 6;

        private void LateUpdate()
        {
            if (!_player.GetWin())
            {
                _targetPosition = new Vector3(_characterPosition.position.x + 3f, _characterPosition.position.y,
                    transform.position.z);
                _smoothedPosition = Vector3.Lerp(transform.position, _targetPosition , _smoothSpeed * Time.deltaTime);
                transform.position = _smoothedPosition;
            }
        }
    }
}


