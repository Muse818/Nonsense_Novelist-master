
using System;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    private static GameInstance _mInstance = null;
    public static GameInstance Instance => _mInstance;

    [RuntimeInitializeOnLoadMethod]
    public static void OnStart()
    {
        if (_mInstance)
        {
            DestroyImmediate(_mInstance);
        }

        _mInstance = new GameObject("GameInstance", typeof(GameInstance)).GetComponent<GameInstance>();
        DontDestroyOnLoad(_mInstance);
        
        Debug.Log("Init GameInstance.");
    }

    private void Awake()
    {
        TableManager.ReadTables();
    }
}