using UnityEngine;

namespace Bullet
{
    public class Bullet: MonoBehaviour
    {

        [SerializeField] private float _speed = 5;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void ShootBullet(Vector3 characterPosition)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = new Vector3(characterPosition.x + 0.5f, characterPosition.y,
                                                characterPosition.z);
            _rigidbody2D.AddForce(new Vector2((transform.position.x + 1) - transform.position.x, transform.position.y) * _speed, ForceMode2D.Impulse);
            
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            gameObject.SetActive(false);
        }
    }
}

