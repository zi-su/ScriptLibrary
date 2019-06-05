using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonobehaviour<T> : MonoBehaviour where T:Object
{
    static T _instance;
    static public T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }
            return _instance;
        }
    }
}
