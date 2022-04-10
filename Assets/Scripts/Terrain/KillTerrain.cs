using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class KillTerrain : MonoBehaviour
{
    [SerializeField] private Player _player; 
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            _player.Respawn();
        }
    }
}
