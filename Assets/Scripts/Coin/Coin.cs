using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            SceneChangerManager.Instance.SetScore(SceneChangerManager.Instance.GetScore() + _score);
            Destroy(gameObject);
        }
    }
}
