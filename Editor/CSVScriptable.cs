using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

/// <summary>
/// CSVフォーマット
/// 型	int	ID	int	int	string	bool
/// 変数名 id  Id atk def effect  active
/// 武器１ 0	WEAPON1	10	20	A TRUE
/// </summary>
public class CSVScriptable : AssetPostprocessor
{
    static public string dataPath = "Assets/Data/GameData/";
    static public string sourcePath = "Assets/Script/ScriptableObject";

    static public string templateHeader = "using System.Collections;" + System.Environment.NewLine +
        "using System.Collections.Generic;" + System.Environment.NewLine +
        "using UnityEngine;" + System.Environment.NewLine +
        "public class {0} : ScriptableBase" + System.Environment.NewLine +
        "{{";

    static public bool Check(string path)
    {
        //指定フォルダー
        if (!path.Contains(dataPath))
        {
            return false;
        }
        //拡張子がcsv
        var ext = Path.GetExtension(path);
        if (ext != ".csv")
        {
            return false;
        }
        return true;
    }

    void OnPreprocessAsset()
    {
        //インポートアセットパスをチェック
        var assetPath = assetImporter.assetPath;
        CreateScriptableData.Create(assetPath);
    }
}
