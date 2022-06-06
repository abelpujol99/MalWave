using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private UnityEngine.Camera _camera;

    [SerializeField] private Vector3 _offset;
    
    [SerializeField] private GameObject _cube;

    private Dictionary<int, Queue<GameObject>> _cubePool;

    private List<List<GameObject>> _cubesList;

    private float[] _heightPlatform;
    private float _cubeWidth;
    private float _cubeHeight;
    private float _height = 8.4f;
    private float _maxNumberOfRandomOperations = 100;

    private int[] _platformWidthPerEachHeight = { 111, 60, 60, 60 };
    private int[] _heightChances = { 2, 98, 0, 0 };
    private int[] _repeatValuePerEachHeight;
    private int[] _minWidthBetweenHolesPerEachHeight = { 20, 7, 7, 7};
    private int[] _maxWidthBetweenHolesPerEachHeight = { 50, 15, 15, 15};
    private int[] _minWidthHolePerEachHeight = { 7, 20, 20, 20};
    private int[] _maxWidthHolePerEachHeight = { 15, 30, 30, 30};
    private int[] _currentMinWidthBetweenHoles;
    private int _minPlatformWidth;
    private int _maxPlatformWidth;
    private int _timesToRepeatCell;

    private void Start()
    {
        Vector3 sprite = _cube.GetComponent<SpriteRenderer>().bounds.size;
        _cubeWidth = sprite.x - 0.0326f;
        _cubeHeight = sprite.y - 0.0326f;
        _offset = new Vector3(_offset.x - _cubeWidth / 2, _offset.y - _cubeHeight / 2, 0);
        _heightPlatform = new float[4];
        for (int i = 0; i < _heightPlatform.Length; i++)
        {
            _heightPlatform[i] = _offset.y + 0.84f * i;
        }
        _repeatValuePerEachHeight = new int[_platformWidthPerEachHeight.Length];
        _currentMinWidthBetweenHoles = new int[_heightPlatform.Length];
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
            SpawnObject(0, _offset.x, _offset.y);
            _offset.x += _cubeWidth;
        }
    }

    private void Update()
    {
        GenerateScenario();
    }
    
    void GenerateScenario()
    {
        for (int i = 0; i < _cubePool.Count; i++)
        {
            Vector3 viewPos = _camera.WorldToViewportPoint(_cubePool[i].Peek().transform.position);
            if (viewPos.x < 0f){
            
                if(_repeatValuePerEachHeight[i] == 0)
                {
                    if (_currentMinWidthBetweenHoles[i] == 0)
                    {
                        var test = Random.Range(0, _maxNumberOfRandomOperations + 1);
                        if (test < _heightChances[0])
                        {
                            _repeatValuePerEachHeight[i] = Random.Range(_minWidthHolePerEachHeight[i], _maxWidthHolePerEachHeight[i] + 1);
                            _currentMinWidthBetweenHoles[i] = Random.Range(_minWidthBetweenHolesPerEachHeight[i], _maxWidthBetweenHolesPerEachHeight[i]);
                        }
                    }
                    else if (_currentMinWidthBetweenHoles[i] != 0)
                    {
                        _currentMinWidthBetweenHoles[i]--;
                    }
                    SpawnObject(i, _offset.x, _heightPlatform[i]);
                }
                else
                {
                    _repeatValuePerEachHeight[i]--;
                }

                _offset.x += _cubeWidth;
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
    
    void SpawnObject(int i, float width, float height) //What ever we spawn will be a child of our procedural generation gameObj
    {
        GameObject obj = _cubePool[i].Dequeue();
        obj.transform.position = new Vector3(width, height);
        obj.SetActive(true);
        _cubePool[i].Enqueue(obj);
    }
}
