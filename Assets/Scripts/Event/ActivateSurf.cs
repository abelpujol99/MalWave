using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Main;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateSurf : MonoBehaviour
{

    [SerializeField] private Player _player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 9)
        {
            Rigidbody2D rigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
            rigidbody2D.gravityScale = 0.22f;
            _player = collision.gameObject.GetComponent<Player>();
            _player.SetSurf(true);
        }
    }
}
