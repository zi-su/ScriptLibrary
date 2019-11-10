using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class CSV2ScriptableObject : AssetPostprocessor
{
    string csvPath = "Assets/Data/GameData";

    void OnPreprocessAsset()
    {
        //インポートアセットパスをチェック
        var assetPath = assetImporter.assetPath;
        MakeScriptableObjectClass.CreateDataScriptable(assetPath);
    }
}
