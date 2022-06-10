using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{

    [SerializeField] private UnityEngine.Camera _camera;

    [SerializeField] private Rigidbody2D _rb2D;

    private Queue<GameObject> _backgroundPool;

    [SerializeField] private GameObject _slicedBackground;

    [SerializeField] private Vector3 _offset;

    private float _backgroundSpeed = -4;
    private float _slicedBackgroundWidth;
    private float _slicedBackgroundHeight;
    private float _initOffsetY;

    private int _columns = 8;
    private int _rows = 5;

    void Start()
    {
        _initOffsetY = _offset.y;
        _rb2D.velocity = new Vector2(_backgroundSpeed, 0);
        Vector3 spriteSizes = _slicedBackground.GetComponent<SpriteRenderer>().bounds.size;
        _slicedBackgroundWidth = spriteSizes.x;
        _slicedBackgroundHeight = spriteSizes.y;
        InitBackgroundPool();
        InitBackground();
    }

    void InitBackgroundPool()
    {
        _backgroundPool = new Queue<GameObject>();
        for (int i = 0; i < _rows * _columns; i++)
        {
            GameObject obj = Instantiate(_slicedBackground);
            obj.transform.parent = transform;
            obj.SetActive(false);
            _backgroundPool.Enqueue(obj);
        }
    }

    void InitBackground()
    {
        for (int i = 0; i < _columns; i++)
        {
            for (int j = 0; j < _rows; j++)
            {
                GameObject obj = _backgroundPool.Dequeue();
                obj.transform.position = new Vector3(_offset.x, _offset.y, _offset.z);
                obj.SetActive(true);
                _backgroundPool.Enqueue(obj);
                _offset.y += _slicedBackgroundHeight;
            }
            _offset.y = _initOffsetY;
            _offset.x += _slicedBackgroundWidth;
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float viewPos = _camera.transform.position.x - _camera.orthographicSize - _slicedBackgroundWidth * 2;
        if (viewPos > _backgroundPool.Peek().transform.position.x)
        {
            transform.position = new Vector3(transform.position.x + _slicedBackgroundWidth, transform.position.y,
                transform.position.z);
        }
    }
}
