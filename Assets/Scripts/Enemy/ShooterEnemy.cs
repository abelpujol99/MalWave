using Characters.Main;
using UnityEngine;

namespace Enemy
{
    public class ShooterEnemy : MonoBehaviour
    {
        private float _timeBetweenShots;
        public float startTimeBtwShots;
        public GameObject Projectile;
        //PLAYER
        [SerializeField] private Player _player;
    
        // Start is called before the first frame update
        void Start()
        {
            _timeBetweenShots = startTimeBtwShots;
        }

        // Update is called once per frame
        void Update()
        {
            if(_timeBetweenShots <= 0){
                Instantiate(Projectile, transform.position, Quaternion.identity);//spawnea un proyectil
                _timeBetweenShots = startTimeBtwShots; 
            }else{
                _timeBetweenShots -= Time.deltaTime;
            }
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 7 && _player.GetDash())
            {
                Destroy(gameObject);
            }
            //else if (other.gameObject.layer == 7 && !_player.ReturnDash())
            //{
            //_player.Death();
            //}
        }
    }
}



