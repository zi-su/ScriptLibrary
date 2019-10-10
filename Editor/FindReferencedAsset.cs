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
        foreach (var guid in guids)
        {
            var targetPath = AssetDatabase.GUIDToAssetPath(guid);
            var allassetpath = AssetDatabase.GetAllAssetPaths();
            foreach (var a in allassetpath)
            {
                var dependencies = AssetDatabase.GetDependencies(a);
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
