using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Main;
using Event;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerControlAdvise : MonoBehaviour
{
    [SerializeField] private GameObject _controlAdvisePanel;

    private void Start()
    {
        if (!SceneChangerManager.Instance.GetActivateTutorial())
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 9)
            {
                Rigidbody2D rigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
                rigidbody2D.gravityScale = 0.22f;
                Player player = collision.gameObject.GetComponent<Player>();
                player.SetSurf(true);
            }
            _controlAdvisePanel.SetActive(true);
            Destroy(gameObject);
        }
    }
}
