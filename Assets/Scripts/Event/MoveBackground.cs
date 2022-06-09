using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{

    [SerializeField] private UnityEngine.Camera _camera;

    [SerializeField] private Rigidbody2D _rb2D;

    [SerializeField] private GameObject _slicedBackground;

    private float _slicedBackgroundWidth;
    private float _backgroundSpeed = -4;

    private int _columns = 8;
    private int _rows = 5;

    // Start is called before the first frame update
    void Start()
    {
        _rb2D.velocity = new Vector2(_backgroundSpeed, 0);
        Vector3 spriteSizes = _slicedBackground.GetComponent<SpriteRenderer>().bounds.size;
        _slicedBackgroundWidth = spriteSizes.x;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (_camera.transform.position.x - transform.position.x > 9.5f)
        {
            transform.position = new Vector3(transform.position.x + _slicedBackgroundWidth, transform.position.y,
                transform.position.z);
        }
    }
}
