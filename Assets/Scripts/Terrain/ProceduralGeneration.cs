using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private UnityEngine.Camera _camera;

    [SerializeField] private Vector3 _offset;
    
    [SerializeField] private GameObject _cube;

    private Renderer _cubeRenderer;

    private Queue<GameObject> _cubePool;

    private float _cubeWidth;
    private float _cubeHeight;
    private float _height = 8.4f;
    private float _width = 111;
    
    private int _minPlatformWidth;
    private int _maxPlatformWidth;
    private int _timesToRepeatCell;
    private int _repeatValue;

    private void Start()
    {
        _cubeRenderer = _cube.GetComponent<Renderer>();
        Vector3 sprite = _cube.GetComponent<SpriteRenderer>().bounds.size;
        _cubeWidth = sprite.x - 0.0326f;
        _cubeHeight = sprite.y - 0.0326f;
        _offset = new Vector3(_offset.x - _cubeWidth / 2, _offset.y - _cubeHeight / 2, 0);
        _cubePool = new Queue<GameObject>();
        FillCubeQueue();
        InitScenario();
    }

    void FillCubeQueue()
    {
        for (int i = 0; i < _width; i++)
        {
            GameObject obj = Instantiate(_cube);
            obj.SetActive(false);
            obj.transform.parent = transform;
            _cubePool.Enqueue(obj);
        }
    }
    void InitScenario()
    {
        for (int i = 0; i < _width; i++)
        {
            SpawnObject(_offset.x, _offset.y);
            _offset.x += _cubeWidth;
        }
    }

    private void Update()
    {
        GenerateScenario();
        //DestroyScenario();
    }
    
    void GenerateScenario()
    {
        Vector3 viewPos = _camera.WorldToViewportPoint(_cubePool.Peek().transform.position);
        if (viewPos.x < 0f)
        {
            SpawnObject(_offset.x, _offset.y);
            _offset.x += _cubeWidth;
        }
        /*for (int x = 0; x < _width; x++) //this will help spawn a tile on the x axis
        {
            SpawnObject(_offset.x + _cubeWidth * x, _offset.y);
        }*/
    }

    void DestroyScenario()
    {
        for (float x = _offset.x - _width * 2; x < _offset.x - _width; x++) //this will help spawn a tile on the x axis
        {
            for (float y = _offset.y; y < _height + _offset.y; y++) //this will help spawn a tile on the y axis
            {
                //_tilemap.SetTile(new Vector3Int(x, y, 0), null); 
                
            }
        }
    }

    void GenerateFlatPlatform(int x)
    {
        for (int y = 0; y < _height; y++) //this will help spawn a tile on the y axis
        {
            //_tilemap.SetTile(new Vector3Int(_offset.x + x, _offset.y + y, 0), _cube); 
                
        }
    }
    
    void SpawnObject(float width, float height) //What ever we spawn will be a child of our procedural generation gameObj
    {
        GameObject obj = _cubePool.Dequeue();
        obj.transform.position = new Vector3(width, height);
        obj.SetActive(true);
        _cubePool.Enqueue(obj);
    }
}
