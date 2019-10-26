using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : class, new()
{
    static readonly T _instance = new T();
    static public T Instance()
    {
        return _instance;
    }
}
