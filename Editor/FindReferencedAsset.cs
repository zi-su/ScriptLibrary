using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;
public class FindReferencedAsset : Editor
{
    [MenuItem("test/find")]
    static void Find()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        var guids = Selection.assetGUIDs;
        var allassetpath = AssetDatabase.GetAllAssetPaths();
        foreach (var guid in guids)
        {
            var targetPath = AssetDatabase.GUIDToAssetPath(guid);    
            foreach (var a in allassetpath)
            {
                var dependencies = AssetDatabase.GetDependencies(a, false);
                foreach (var d in dependencies)
                {
                    if(string.Equals(targetPath, d))
                    {
                        Debug.Log(a);
                    }
                }
            }
        }
        sw.Stop();
        Debug.Log(sw.ElapsedMilliseconds);
    }
}
