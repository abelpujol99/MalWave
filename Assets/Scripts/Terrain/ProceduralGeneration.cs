using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Main;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private UnityEngine.Camera _camera;
    
    [SerializeField] private Vector3 _offset;
    
    [SerializeField] private GameObject _cube;
    [SerializeField] private GameObject _boss;
    
    private Dictionary<int, Queue<GameObject>> _cubePool;

    private List<List<GameObject>> _cubesList;
    
    private float[] _heightPlatform;
    private float _height = 1.2f;
    private float _maxNumberOfRandomOperations = 100;
    private float _cubeWidth;
    private float _cubeHeight;

    private int[] _platformWidthPerEachHeight = { 121, 200, 200, 200 };
    private int[] _heightChances = { 2, 60, 0, 0 };
    private int[] _minWidthPlatformPerEachHeightOverFloor = {30, 20, 15};
    private int[] _maxWidthPlatformPerEachHeightOverFloor = {40, 35, 30};
    private int[] _minSeparationBetweenPlatformsPerEachHeightOverFloor = {10, 13, 17};
    private int[] _maxSeparationBetweenPlatformsPerEachHeightOverFloor = {25, 25, 30};
    private int[] _minPositionDelayToAppearPlatformOverFirstHeightPlatform = {0, 20, 20}; //20, 30
    private int[] _maxPositionDelayToAppearPlatformOverFirstHeightPlatform = {0, 40, 40}; // 30, 35
    private int[] _positionsDelayToAppearPlatformOverFirstHeightPlatform;
    private int[] _lastPositionDelayToAppearPlatformOverFirstHeightPlatform;
    private int[] _repeatValuePerEachHeight;
    private int[] _currentMinSeparationBetweenPlatforms;

    private int _minWidthHole = 7;
    private int _maxWidthHole = 15;
    private int _minSeparationBetweenHoles = 20;
    private int _maxSeparationBetweenHoles = 50;
    private int _currentMinSeparationBetweenHoles;
    
    private bool _bossSpawn;

    private void Start()
    {
        Vector3 spriteSizes = _cube.GetComponent<SpriteRenderer>().bounds.size;
        _cubeWidth = spriteSizes.x - 0.0326f;
        _cubeHeight = spriteSizes.y - 0.0326f;
        _offset = new Vector3(_offset.x - _cubeWidth / 2, _offset.y - _cubeHeight / 2, 0);
        _heightPlatform = new float[_platformWidthPerEachHeight.Length];
        for (int i = 0; i < _heightPlatform.Length; i++)
        {
            _heightPlatform[i] = _offset.y + _height * i;
        }
        _repeatValuePerEachHeight = new int[_platformWidthPerEachHeight.Length];
        _currentMinSeparationBetweenPlatforms = new int[_heightPlatform.Length - 1];
        _positionsDelayToAppearPlatformOverFirstHeightPlatform = new int[_heightChances.Length - 1];
        _lastPositionDelayToAppearPlatformOverFirstHeightPlatform =
            new int[_positionsDelayToAppearPlatformOverFirstHeightPlatform.Length];
        SetCubesList();
        FillCubeDictionary();
        InitScenario();
    }


    void SetCubesList()
    {
        _cubesList = new List<List<GameObject>>();
        int counter = 0;

        for (int i = 0; i < _heightPlatform.Length; i++)
        {
            List<GameObject> objectList = new List<GameObject>();
            for (int j = 0; j < _platformWidthPerEachHeight[counter]; j++)
            {
                objectList.Add(_cube);
            }
            _cubesList.Add(objectList);
            counter++;
        }
    }


    void FillCubeDictionary()
    {
        _cubePool = new Dictionary<int, Queue<GameObject>>();

        int counter = 0;
        
        foreach (List<GameObject> listCube in _cubesList)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();
            
            foreach (GameObject cube in listCube)
            {

                GameObject obj = Instantiate(cube);
                obj.transform.parent = transform;
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }
            
            _cubePool.Add(counter, objectQueue);

            counter++;
        }
    }

    void InitScenario()
    {
        for (int i = 0; i < _platformWidthPerEachHeight[0]; i++)
        {
            MoveCube(0, _offset.x, _offset.y);
            _offset.x += _cubeWidth;
        }
    }

    void WhileBossApproaching()
    {
        Vector3 viewPos = _camera.WorldToViewportPoint(_cubePool[0].Peek().transform.position);
        if (viewPos.x < 0f)
        {
            MoveCube(0, _offset.x, _heightPlatform[0]);
            _offset.x += _cubeWidth;
        }
    }

    private void Update()
    {
        if (_boss.activeSelf)
        {
            GenerateScenario();
        }
        else
        {
            WhileBossApproaching();
        }
    }


    void GenerateScenario()
    {
        for (int i = 0; i < _cubePool.Count; i++)
        {
            Vector3 viewPos = _camera.WorldToViewportPoint(_cubePool[0].Peek().transform.position);
            if (viewPos.x + _cubeWidth < 0f)
            {
                if (i == 0)
                {
                    GenerateFloor(i);
                }
                else
                {
                    GeneratePlatforms(i);
                }
            
            }     
        }

        for (int i = 2; i < _heightChances.Length; i++)
        {
            _heightChances[i] = 0;
        }
    }

    void GenerateFloor(int i)
    {

        if (_repeatValuePerEachHeight[i] == 0)
        {
            if (_currentMinSeparationBetweenHoles == 0)
            {
                if (Random.Range(0, _maxNumberOfRandomOperations + 1) > _heightChances[i])
                {
                    _repeatValuePerEachHeight[i] = Random.Range(_minWidthHole,
                        _maxWidthHole + 1);

                    _currentMinSeparationBetweenHoles = Random.Range(
                        _minSeparationBetweenHoles,
                        _maxSeparationBetweenHoles + 1);
                }
            }
            else
            {
                _currentMinSeparationBetweenHoles--;
            }
            MoveCube(i, _offset.x, _heightPlatform[i]);
        }
        else
        {
            _repeatValuePerEachHeight[i]--;
        }
        
        _offset.x += _cubeWidth;
    }

    void GeneratePlatforms(int i)
    {
        if (_currentMinSeparationBetweenPlatforms[i - 1] == 0)
        {
            if (Random.Range(0, _maxNumberOfRandomOperations + 1) < _heightChances[i])
            {
                _repeatValuePerEachHeight[i] = Random.Range(_minWidthPlatformPerEachHeightOverFloor[i - 1],
                    _maxWidthPlatformPerEachHeightOverFloor[i - 1] + 1);

                _currentMinSeparationBetweenPlatforms[i - 1] = Random.Range(
                    _minSeparationBetweenPlatformsPerEachHeightOverFloor[i - 1],
                    _maxSeparationBetweenPlatformsPerEachHeightOverFloor[i - 1] + 1);

                if (i < _heightChances.Length - 1)
                {
                    _heightChances[i + 1] = 60;
                }
                
                if (_positionsDelayToAppearPlatformOverFirstHeightPlatform[i - 1] == 0)
                {
                    _positionsDelayToAppearPlatformOverFirstHeightPlatform[i - 1] = Random.Range(
                            _minPositionDelayToAppearPlatformOverFirstHeightPlatform[i - 1],
                            _maxPositionDelayToAppearPlatformOverFirstHeightPlatform[i - 1]) +
                            _lastPositionDelayToAppearPlatformOverFirstHeightPlatform[i - 1];

                    if (i < _lastPositionDelayToAppearPlatformOverFirstHeightPlatform.Length)
                    {
                        _lastPositionDelayToAppearPlatformOverFirstHeightPlatform[i] =
                            _positionsDelayToAppearPlatformOverFirstHeightPlatform[i - 1];
                    }
                }
                
                var counter = 0;
                do
                {
                    if (_positionsDelayToAppearPlatformOverFirstHeightPlatform[i - 1] == 0)
                    {
                        MoveCube(i, _offset.x + counter * _cubeWidth, _heightPlatform[i]);
                        _repeatValuePerEachHeight[i]--;
                    }
                    else
                    {
                        _positionsDelayToAppearPlatformOverFirstHeightPlatform[i - 1]--;
                    }
                    counter++;
                    
                } while (_repeatValuePerEachHeight[i] != 0);
            }
            else
            {
                if (i < _heightChances.Length - 1)
                {
                    _heightChances[i + 1] = 0;
                }
            }
        }
        else
        {
            _currentMinSeparationBetweenPlatforms[i - 1]--;
        }
    }
   
    void MoveCube(int i, float width, float height) 
    {
        GameObject obj = _cubePool[i].Dequeue();
        obj.transform.position = new Vector3(width, height);
        obj.SetActive(true);
        _cubePool[i].Enqueue(obj);
    }
}