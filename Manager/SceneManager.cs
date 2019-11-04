using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class SceneManager : Singleton<SceneManager>
{
    UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle handle;
    bool isLoading;

    public bool IsLoading()
    {
        return isLoading;
    }
    public void LoadScene(object key)
    {
        isLoading = true;
        if (handle.IsValid())
        {
            Addressables.Release(handle);
        }
        Fade.Instance().Play(Fade.Mode.Out, Color.black, new Color(0.0f, 0.0f, 0.0f, 0.0f), action:()=> {
            handle = Addressables.LoadSceneAsync(key);
            handle.Completed += Handle_Completed;
        });
    }

    private void Handle_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle obj)
    {
        isLoading = false;
        Fade.Instance().Play(Fade.Mode.In, new Color(0.0f, 0.0f, 0.0f, 0.0f));
        Resources.UnloadUnusedAssets();
    }
}
