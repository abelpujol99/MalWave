using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class RockEnemy : MonoBehaviour
{

    [SerializeField] private Player _player;
    
    
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.layer == 7 && _player.ReturnDash())
        {
            Destroy(gameObject);
        }
    }
}
