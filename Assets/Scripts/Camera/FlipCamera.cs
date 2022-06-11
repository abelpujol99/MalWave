using UnityEngine;

namespace Camera
{
    public class FlipCamera : MonoBehaviour
    {
        private Matrix4x4 mat;
    
        void Start()
        {
            mat = UnityEngine.Camera.main.projectionMatrix;
            mat *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
            UnityEngine.Camera.main.projectionMatrix = mat;
        }

        void Update()
        {
        
        }
    }
    
}
