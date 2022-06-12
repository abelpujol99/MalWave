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

    [SerializeField] private UIMenu _mainCanvas;

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
            _mainCanvas.SetTutorialActive(true);
            _controlAdvisePanel.SetActive(true);
            Destroy(gameObject);
        }
    }
}
