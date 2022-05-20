using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class TriggerControlAdvise : MonoBehaviour
{
    [SerializeField] private GameObject _controlAdvisePanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            _controlAdvisePanel.SetActive(true);
            Destroy(gameObject);
        }
    }
}
