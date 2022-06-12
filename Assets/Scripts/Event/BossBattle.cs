using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Boss;
using TMPro;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    [SerializeField] private UnityEngine.Camera _mainCamera;
    [SerializeField] private UnityEngine.Camera _cameraUI;

    [SerializeField] private SpriteRenderer _warningSprite;
    
    [SerializeField] private TextMeshProUGUI _bossApproachingText;

    [SerializeField] private AudioSource _warninigAudio;

    [SerializeField] private GameObject _panel;

    private Color opacity;

    [SerializeField] private GameObject _boss;
 
    private float _timeToEndWarning;
    private float _timeToBlink;
    private float _currentTimeToBlink;
    private float _mainCameraPositionX;

    private void Start()
    {
        opacity = new Color(255, 241, 0, 255);
        _timeToEndWarning = 6;
        _timeToBlink = 2f;
        _currentTimeToBlink = _timeToBlink;
    }

    void MoveCamera()
    {
        _mainCameraPositionX = _mainCamera.transform.position.x;
        _cameraUI.transform.position = new Vector3(_mainCameraPositionX, _cameraUI.transform.position.y, _cameraUI.transform.position.z);
        transform.position = new Vector3(_mainCameraPositionX, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_timeToEndWarning > 0)
        {
            _timeToEndWarning -= 1 * Time.deltaTime;
            BlinkWarning();
        }
        else
        {
            if (_boss != null)
            {
                _boss.SetActive(true);
            }
            MoveCamera();
            Destroy(_panel);
        }

    }

    void BlinkWarning()
    {
        if (_currentTimeToBlink > 0)
        {
            _currentTimeToBlink -= 1 * Time.deltaTime;
            opacity.a = 0 + (_currentTimeToBlink - 0) * (1 - 0) / (_timeToBlink - 0);
            _warningSprite.color = opacity;
            _bossApproachingText.color = opacity;
        }
        else
        {
            _currentTimeToBlink = _timeToBlink;
            opacity.a = 1;
            _warningSprite.color = opacity;
            _bossApproachingText.color = opacity;
        }
    }
}
