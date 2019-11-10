using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class CreateScriptableData : MonoBehaviour
{
    [MenuItem("Assets/CreateDataScriptable")]
    static public void Create()
    {
        foreach (var s in Selection.objects)
        {
            var path = AssetDatabase.GetAssetPath(s);
            if (CSVScriptable.Check(path))
            {
                //CSVファイル名と同じスクリプタブルオブジェクトを作成
                var filename = Path.GetFileNameWithoutExtension(path);
                var so = ScriptableObject.CreateInstance(filename);
                AssetDatabase.CreateAsset(so, CSVScriptable.dataPath + $"{filename}.asset");
                AssetDatabase.Refresh();
                var sb = AssetDatabase.LoadAssetAtPath<ScriptableBase>(CSVScriptable.dataPath + $"{filename}.asset");
                var lines = File.ReadAllLines(path);
                sb.Convert(lines);
            }
        }
    }

    static public void Create(string path)
    {
        if (CSVScriptable.Check(path))
        {
            //CSVファイル名と同じスクリプタブルオブジェクトを作成
            var filename = Path.GetFileNameWithoutExtension(path);
            if (File.Exists(CSVScriptable.dataPath + $"{filename}.asset"))
            {
                var sb = AssetDatabase.LoadAssetAtPath<ScriptableBase>(CSVScriptable.dataPath + $"{filename}.asset");
                var lines = File.ReadAllLines(path);
                sb.Convert(lines);
            }
            else
            {
                if (!File.Exists(CSVScriptable.sourcePath + $"{filename}.cs"))
                {
                    CreateScriptableSource.Create(path);
                    AssetDatabase.Refresh();
                    return;
                }
                var so = ScriptableObject.CreateInstance(filename);
                AssetDatabase.CreateAsset(so, CSVScriptable.dataPath + $"{filename}.asset");
                AssetDatabase.Refresh();
                var sb = AssetDatabase.LoadAssetAtPath<ScriptableBase>(CSVScriptable.dataPath + $"{filename}.asset");
                var lines = File.ReadAllLines(path);
                sb.Convert(lines);
            }

        }
    }
}
