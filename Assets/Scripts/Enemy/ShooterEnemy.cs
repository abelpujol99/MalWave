using Characters.Main;
using UnityEngine;

namespace Enemy
{
    public class ShooterEnemy : MonoBehaviour
    {
        private float _timeBetweenShots;
        private float _startTimeBetweenShots =  2;
        [SerializeField] private GameObject _projObject;

        private Projectile _projectile;
        //PLAYER
        [SerializeField] private Player _player;
    
        // Start is called before the first frame update
        void Start()
        {
            _timeBetweenShots = _startTimeBetweenShots;
        }

        // Update is called once per frame
        void Update()
        {
            if(_timeBetweenShots <= 0){
                GameObject obj = Instantiate(_projObject, transform.position, Quaternion.identity);//spawnea un proyectil
                _projectile = obj.GetComponent<Projectile>();
                _timeBetweenShots = _startTimeBetweenShots; 
                _projectile.Shoot(gameObject, _player);
            }else{
                _timeBetweenShots -= Time.deltaTime;
            }
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 7 && _player.GetDash() || other.gameObject.layer == 8)
            {
                Destroy(gameObject);
                if (other.gameObject.layer == 8){
                    other.gameObject.SetActive(false);
                }
            }
            else if (other.gameObject.layer == 7)
            {
            _player.Death(gameObject.name.Split('(')[0]);
            }
        }
    }
}



