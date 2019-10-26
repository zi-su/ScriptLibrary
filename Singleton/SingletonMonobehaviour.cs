using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonobehaviour<T> : MonoBehaviour where T:Component
{
    static T _instance = null;
    static public T Instance()
    {
        if(_instance == null)
        {
            var go = new GameObject();
            _instance = go.AddComponent<T>();
        }
        return _instance;
    }

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
