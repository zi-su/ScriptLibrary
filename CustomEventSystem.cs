using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CustomEventSystem : SingletonMonobehaviour<CustomEventSystem>
{
    EventSystem _current;
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

    public EventSystem Current
    {
        get { return _current; }
    }
}
