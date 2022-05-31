using UnityEngine;

namespace Event
{
    public class DontDestroyMusic : MonoBehaviour
    {

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}

