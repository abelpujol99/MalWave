using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class RockEnemy : MonoBehaviour
{

    [SerializeField] private Player _player;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7 && _player.ReturnDash())
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == 7 && !_player.ReturnDash())
        {
            _player.Death(gameObject.name.Split('(')[0]);
        }
    }

}
