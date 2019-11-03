using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement;
using UnityEngine.AddressableAssets;
public class LoadList
{
    List<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle> handles = new List<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle>();

    public void Add<T>(object key, System.Action<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<T>> onComplete = null)
    {
        var h = Addressables.LoadAssetAsync<T>(key);
        h.Completed += onComplete;
        handles.Add(h);
    }

    public object Get(int index)
    {
        if(index < 0 || index > handles.Count - 1)
        {
            Debug.LogAssertion("index out of range");
        }
        if (handles[index].IsDone)
        {
            return handles[index].Result;
        }
        return null;
    }

    public bool IsLoading()
    {
        foreach (var item in handles)
        {
            if (!item.IsDone)
            {
                return false;
            }
        }
        return true;
    }

    public void Release()
    {
        foreach (var item in handles)
        {
            Addressables.Release(item);
        }
        handles.Clear();
    }

    public void Release(int index)
    {
        if (index < 0 || index > handles.Count - 1)
        {
            Debug.LogAssertion("index out of range");
        }
        var h = handles[index];
        Addressables.Release(h);
        handles.Remove(h);
    }
}
