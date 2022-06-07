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

    [SerializeField] private GameObject _lastCubeSpawned;

    private Dictionary<int, Queue<GameObject>> _cubePool;

    private List<List<GameObject>> _cubesList;
    
    private float[] _heightPlatform;
    private float _height = 1.2f;
    private float _maxNumberOfRandomOperations = 100;
    private float _cubeWidth;
    private float _cubeHeight;

    private int[] _platformWidthPerEachHeight = { 111, 60, 60, 60 };
    private int[] _heightChances = { 98, 2, 0, 0 };
    private int[] _minWidthHolePerEachHeight = { 7, 20, 20, 20};
    private int[] _maxWidthHolePerEachHeight = { 15, 30, 30, 30};
    private int[] _minSeparationBetweenHolesPerEachHeight = { 20, 7, 7, 7};
    private int[] _maxSeparationBetweenHolesPerEachHeight = { 50, 15, 15, 15};
    private int[] _repeatValuePerEachHeight;
    private int[] _currentMinSeparationBetweenHoles;
    
    private bool _bossSpawn;

    private void Start()
    {
        Vector3 sprite = _cube.GetComponent<SpriteRenderer>().bounds.size;
        _cubeWidth = sprite.x - 0.0326f;
        _cubeHeight = sprite.y - 0.0326f;
        _offset = new Vector3(_offset.x - _cubeWidth / 2, _offset.y - _cubeHeight / 2, 0);
        _heightPlatform = new float[4];
        for (int i = 0; i < _heightPlatform.Length; i++)
        {
            _heightPlatform[i] = _offset.y + _height * i;
        }
        _repeatValuePerEachHeight = new int[_platformWidthPerEachHeight.Length];
        _currentMinSeparationBetweenHoles = new int[_heightPlatform.Length];
        //SetCubeQueue();
        SetCubesList();
        FillCubeDictionary();
        InitScenario();
    }

    /*void SetCubeQueue()
    {
        _cubePool = new Queue<GameObject>();
        for (int i = 0; i < _platformWidthPerEachHeight[0]; i++)
        {
            GameObject obj = Instantiate(_cube);
            obj.SetActive(false);
            _cubePool.Enqueue(obj);
        }
    }*/



    void InitScenario()
    {
        for (int i = 0; i < _platformWidthPerEachHeight[0]; i++)
        {
            SpawnObject(0, _offset.x, _offset.y);
            _offset.x += _cubeWidth;
        }
    }

    private void Update()
    {
        Vector3 viewPos = _camera.WorldToViewportPoint(_cubePool[0].Peek().transform.position);
        if (viewPos.x < 0f)
        {
            GenerateFloor();

            GeneratePlatforms();
        }

        /*if (_player.GetDash() && _player.GetCanDash())
        {
            _dashTargetPosition = new Vector3(transform.position.x - _dashDistance, transform.position.y, 0);
        }*/
    }

    /*private void FixedUpdate()
    {
        if (_player.GetDash())
        {
            DashScenario();
        }
        else
        {
            MoveScenario();
        }
    }

    void MoveScenario()
    {
        if (_currentScenarioSpeed < _scenarioSpeed)
        {
            _currentScenarioSpeed += Time.deltaTime;
        }

        _rb2D.velocity = new Vector2(-_currentScenarioSpeed, _rb2D.velocity.y);
    }

    void DashScenario()
    {
        _rb2D.AddForce(new Vector2(_dashTargetPosition.x - transform.position.x,
            _dashTargetPosition.y - transform.position.y) * _scenarioDashSpeed, ForceMode2D.Impulse);
        if (_dashTargetPosition.x > transform.position.x)
        {
            EndScenarioDash();
        }
    }

    void EndScenarioDash()
    {
        _currentScenarioSpeed = 2.5f;
        _rb2D.velocity = Vector2.zero;
        _rb2D.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
    }*/


    void GenerateFloor()
    {
        //if (_offset.x - _lastCubeSpawned.transform.position.x >= _cubeWidth)
        //Check if first cube of the Queue disappear from the point view
        
            //Check if the cube has to appear
            if (_repeatValuePerEachHeight[0] == 0)
            {
                //Check if hole has no "cooldown"
                if (_currentMinSeparationBetweenHoles[0] == 0)
                {
                    //Check if a hole has to appear
                    if (Random.Range(0, _maxNumberOfRandomOperations + 1) > _heightChances[0])
                    {
                        //Large of the hole
                        _repeatValuePerEachHeight[0] = Random.Range(_minWidthHolePerEachHeight[0],
                            _maxWidthHolePerEachHeight[0] + 1);

                        //New cooldown between holes
                        _currentMinSeparationBetweenHoles[0] = Random.Range(
                            _minSeparationBetweenHolesPerEachHeight[0],
                            _maxSeparationBetweenHolesPerEachHeight[0] + 1);
                    }
                }
                //Check if hole has "cooldown"
                else
                {
                    _currentMinSeparationBetweenHoles[0]--;
                }
                SpawnObject(0, _offset.x, _heightPlatform[0]);
            }
            //Check if the cube has not to appear
            else
            {
                _repeatValuePerEachHeight[0]--;
            }
            _offset.x += _cubeWidth;
        
    }
    
    void GeneratePlatforms()
    {
        for (int i = 1; i < _cubePool.Count; i++)
        {
            //Check if the cube has to appear
            if (_repeatValuePerEachHeight[i] == 0)
            {
                //Check if hole has no "cooldown"
                if (_currentMinSeparationBetweenHoles[i] == 0)
                {
                    //Check if a hole has to appear
                    if (Random.Range(0, _maxNumberOfRandomOperations + 1) > _heightChances[i])
                    {
                        //Large of the hole
                        _repeatValuePerEachHeight[i] = Random.Range(_minWidthHolePerEachHeight[i],
                            _maxWidthHolePerEachHeight[i] + 1);

                        //New cooldown between holes
                        _currentMinSeparationBetweenHoles[i] = Random.Range(
                            _minSeparationBetweenHolesPerEachHeight[i],
                            _maxSeparationBetweenHolesPerEachHeight[i] + 1);
                    }
                }
                //Check if hole has "cooldown"
                else
                {
                    _currentMinSeparationBetweenHoles[i]--;
                }
                SpawnObject(i, _offset.x, _heightPlatform[i]);
            }
            //Check if the cube has not to appear
            else
            {
                _repeatValuePerEachHeight[i]--;
            }
        }
    }

    /*void GenerateScenario()
    {

        if (_camera.WorldToViewportPoint(_cubePool.Peek().transform.position).x < 0f)
        {
            
            
        }
        
        
    }*/

    
    /*void SpawnObject(float width, float height) 
    {
        GameObject obj = _cubePool[i].Dequeue();
        obj.transform.position = new Vector3(width, height);
        obj.SetActive(true);
        _cubePool[i].Enqueue(obj);
    }*/
    
    

    
    void SpawnObject(int i, float width, float height) 
    {
        GameObject obj = _cubePool[i].Dequeue();
        obj.transform.position = new Vector3(width, height);
        obj.SetActive(true);
        _cubePool[i].Enqueue(obj);
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












    /*void GenerateScenario()
    {
    
        //Check if first cube of the Queue disappear from the point view
        Vector3 viewPos = _camera.WorldToViewportPoint(_cubePool[_activeHeight].Peek().transform.position);
        if (viewPos.x < 0f)
        {
            _disappear = true;
            if (_heightChances[_activeHeight] != 0)
            {
                //Check if the cube has to appear
                if(_repeatValuePerEachHeight[_activeHeight] == 0)
                {
                    //Check if hole has no "cooldown"
                    if (_currentMinSeparationBetweenHoles[_activeHeight] == 0)
                    {
                        //Check if a hole has to appear
                        if (Random.Range(0, _maxNumberOfRandomOperations + 1) > _heightChances[0])
                        {
                            //Large of the hole
                            _repeatValuePerEachHeight[_activeHeight] = Random.Range(_minWidthHolePerEachHeight[_activeHeight],
                                _maxWidthHolePerEachHeight[_activeHeight] + 1);

                            //New cooldown between holes
                            _currentMinSeparationBetweenHoles[_activeHeight] = Random.Range(
                                _minSeparationBetweenHolesPerEachHeight[_activeHeight],
                                _maxSeparationBetweenHolesPerEachHeight[_activeHeight] + 1);
                        }
                    }
                    //Check if hole has "cooldown"
                    else
                    {
                        _currentMinSeparationBetweenHoles[_activeHeight]--;
                    }
                    SpawnObject(_activeHeight, _offset.x, _heightPlatform[_activeHeight]);
                }
                //Check if the cube has not to appear
                else
                {
                    _repeatValuePerEachHeight[_activeHeight]--;
                }
            }
            _offset.x += _cubeWidth;
        }
    }*/
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
       
        
        
        
        
        
        
        
        
        
        
        
        
        
    
    
    /*void GenerateScenario()
    {
        //Go over all the Queue
        
        GenerateFloor();
        
        for (int i = 1; i < _cubePool.Count; i++)
        {
            GeneratePlatform(i);
        }

        //Move offset in case some cube disappear
        if (_disappear)
        {
            _offset.x += _cubeWidth;
            _disappear = false;
        }
    }

    void GenerateFloor()
    {
        //Check if first cube of the Queue disappear from the point view
        Vector3 viewPos = _camera.WorldToViewportPoint(_cubePool[0].Peek().transform.position);
        if (viewPos.x < 0f)
        {
            _disappear = true;
            
            //Check if the cube has to appear
            if(_repeatValuePerEachHeight[0] == 0)
            {
                //Check if hole has no "cooldown"
                if (_currentMinWidthBetweenHoles[0] == 0)
                {
                    //Check if a hole has to appear
                    if (Random.Range(0, _maxNumberOfRandomOperations + 1) < _heightChances[0])
                    {
                        _repeatValuePerEachHeight[0] = Random.Range(_minWidthHolePerEachHeight[0], _maxWidthHolePerEachHeight[0] + 1); 
                        _currentMinWidthBetweenHoles[0] = Random.Range(_minWidthBetweenHolesPerEachHeight[0], _maxWidthBetweenHolesPerEachHeight[0] + 1);
                    }
                }
                //Check if hole has "cooldown"
                else
                {
                    _currentMinWidthBetweenHoles[0]--;
                }
                SpawnObject(0, _offset.x, _heightPlatform[0]);
            }
            //Check if the cube has not to appear
            else
            {
                _repeatValuePerEachHeight[0]--;
            }
        }
    }

    void GeneratePlatform(int i)
    {
        //Check if first cube of the Queue disappear from the point view
        Vector3 viewPos = _camera.WorldToViewportPoint(_cubePool[i].Peek().transform.position);
        if (viewPos.x < 0f)
        {
            _disappear = true;
            
            //Check if the cube has to appear
            if(_repeatValuePerEachHeight[i] == 0)
            {
                //Check if hole has no "cooldown"
                if (_currentMinWidthBetweenHoles[i] == 0)
                {
                    //Check if a hole has to appear
                    if (Random.Range(0, _maxNumberOfRandomOperations + 1) < _heightChances[i])
                    {
                        //_repeatValuePerEachHeight[i] = Random.Range(_minWidthHolePerEachHeight[i], _maxWidthHolePerEachHeight[i] + 1);
                        //_currentMinWidthBetweenHoles[i] = Random.Range(_minWidthBetweenHolesPerEachHeight[i], _maxWidthBetweenHolesPerEachHeight[i] + 1);
                            
                        //Large of the hole
                        _repeatValuePerEachHeight[i] = _minWidthHolePerEachHeight[i];
                            
                        //New cooldown between holes
                        _currentMinWidthBetweenHoles[i] = _minWidthBetweenHolesPerEachHeight[i];
                    }
                }
                //Check if hole has "cooldown"
                else
                {
                    _currentMinWidthBetweenHoles[i]--;
                }
                SpawnObject(i, _offset.x, _heightPlatform[i]);
            }
            //Check if the cube has not to appear
            else
            {
                _repeatValuePerEachHeight[i]--;
            }
        }
    }*/
}
