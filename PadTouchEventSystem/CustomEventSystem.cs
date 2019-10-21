using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CustomEventSystem : MonoBehaviour
{
    static EventSystem _current;
    static GameObject _selectedGameObject;

    static public GameObject CurrentSelected
    {
        get { return _selectedGameObject; }
        set { _selectedGameObject = value; }
    }

    static public EventSystem CurrentEventSystem
    {
        get {
            if(_current == null)
            {
                _current = EventSystem.current;
            }
            return _current;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _current = EventSystem.current;
        if(_current == null)
        {
            _current = gameObject.AddComponent<EventSystem>();
        }
    }
}
