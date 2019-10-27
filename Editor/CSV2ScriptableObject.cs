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
        var dirname = Path.GetDirectoryName(assetPath).Replace(Path.DirectorySeparatorChar, '/');
        //スクリプタブルオブジェクト用フォルダでCSVの場合処理する
        if (string.Equals(dirname, csvPath) && Path.GetExtension(assetPath) == ".csv")
        {
            Debug.Log("ConvertStart:" + assetPath);
            //全行取得
            string[] lines = File.ReadAllLines(assetPath);
            string[] split = lines[0].Split(',');
            //フォーマット通りならスクリプタブルオブジェクトのクラス名が入っている
            string classname = split[1];

            //ScriptableObjectアセットを作成or取得
            var filename = Path.GetFileNameWithoutExtension(assetPath).Trim(Path.DirectorySeparatorChar);
            //出力ファイルパス
            var output = Path.Combine(csvPath, filename + ".asset");

            //csv全行からデータをクラスごとにパースしてアセット作成
            Convert(output, classname, lines);

            AssetDatabase.Refresh();
            Debug.Log("ConvertEnd:" + output);
        }
    }

    void Convert(string output, string className, string[] lines)
    {
        ScriptableObject so = null;
        bool isCreate = false;
        //クラス毎にコンバート処理
        if (className == "MyScriptableObject")
        {
            MyScriptableObject mso = CreateOrLoad<MyScriptableObject>(output, out isCreate);
            mso.Convert(lines);
            so = mso;
        }
        if (isCreate)
        {
            AssetDatabase.CreateAsset(so, output);
        }
    }

    /// <summary>
    /// 出力ファイルが存在してれば読み込み、泣ければインスタンス生成して返す
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="output"></param>
    /// <param name="isCreate"></param>
    /// <returns></returns>
    T CreateOrLoad<T>(string output, out bool isCreate) where T:ScriptableObject
    {
        T o = null;
        if (File.Exists(output))
        {
            o = AssetDatabase.LoadAssetAtPath<T>(output);
            isCreate = false;
        }
        else
        {
            o = ScriptableObject.CreateInstance<T>();
            isCreate = true;
        }

        return o;
    }
}
