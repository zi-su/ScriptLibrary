using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    Dictionary<int, Functions> _map = new Dictionary<int, Functions>();

    int _currentId = -1;
    Functions _functions;
    class Functions
    {
        System.Action _begin;
        System.Action _update;
        System.Action _end;
        public Functions(System.Action begin = null, System.Action update = null, System.Action end = null)
        {
            _begin = begin;
            _update = update;
            _end = end;
        }

        public void Begin()
        {
            _begin?.Invoke();
        }
        public void Update()
        {
            _update?.Invoke();
        }
        public void End()
        {
            _end?.Invoke();
        }
    }
    
    public void Add<T>(T id, System.Action update, System.Action begin = null, System.Action end = null) where T : System.Enum
    {
        int i = (int)(id as object);
        _map.Add(i, new Functions(begin, update, end));
    }

    public void Remove<T>(T id) where T:System.Enum
    {
        int i = (int)(id as object);
        _map.Remove(i);
    }

    public void RequestState<T>(T id) where T:System.Enum
    {
        int i = (int)(id as object);
        Functions functions;
        if(_map.TryGetValue(_currentId, out functions))
        {
            functions.End();
        }
        _currentId = i;
        if(_map.TryGetValue(_currentId, out functions))
        {
            functions.Begin();
        }
    }

    public void Update()
    {
        if(_map.TryGetValue(_currentId, out _functions))
        {
            _functions.Update();
        }
    }
}
