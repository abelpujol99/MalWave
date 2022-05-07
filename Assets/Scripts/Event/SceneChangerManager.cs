using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangerManager : MonoBehaviour
{
    private static SceneChangerManager _instance;

    private static int _lastSceneIndex;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public static int GetLastSceneIndex()
    {
        return _lastSceneIndex;
    }

    public static void SetLastSceneIndex(int lastSceneIndex)
    {
        _lastSceneIndex = lastSceneIndex;
    }
}
