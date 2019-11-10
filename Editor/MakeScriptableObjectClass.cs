using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class MakeScriptableObjectClass
{
    static string datapath = "Assets/Data/GameData/";
    static string sourcePath = "Assets/Script/ScriptableObject";

    static string templateHeader = "using System.Collections;"+System.Environment.NewLine+
        "using System.Collections.Generic;" + System.Environment.NewLine +
        "using UnityEngine;" + System.Environment.NewLine +
        "public class {0} : ScriptableBase" + System.Environment.NewLine +
        "{{";
    [MenuItem("Assets/CraeteSourceScriptable")]
    static public void CreateSourceScriptable()
    {
        foreach(var s in Selection.objects)
        {
            var path = AssetDatabase.GetAssetPath(s);
            if (Check(path))
            {
                CreateSource(path);
            }
        }
    }
    [MenuItem("Assets/CreateDataScriptable")]
    static public void CreateDataScriptable()
    {
        foreach (var s in Selection.objects)
        {
            var path = AssetDatabase.GetAssetPath(s);
            if (Check(path))
            {
                //CSVファイル名と同じスクリプタブルオブジェクトを作成
                var filename = Path.GetFileNameWithoutExtension(path);
                var so = ScriptableObject.CreateInstance(filename);
                AssetDatabase.CreateAsset(so, datapath + $"{filename}.asset");
                AssetDatabase.Refresh();
                var sb = AssetDatabase.LoadAssetAtPath<ScriptableBase>(datapath + $"{filename}.asset");
                var lines = File.ReadAllLines(path);
                sb.Convert(lines);
            }
        }
    }

    static public void CreateDataScriptable(string path)
    {
        if (Check(path))
        {
            //CSVファイル名と同じスクリプタブルオブジェクトを作成
            var filename = Path.GetFileNameWithoutExtension(path);
            if(File.Exists(datapath + $"{filename}.asset"))
            {
                var sb = AssetDatabase.LoadAssetAtPath<ScriptableBase>(datapath + $"{filename}.asset");
                var lines = File.ReadAllLines(path);
                sb.Convert(lines);
            }
            else
            {
                var so = ScriptableObject.CreateInstance(filename);
                AssetDatabase.CreateAsset(so, datapath + $"{filename}.asset");
                AssetDatabase.Refresh();
                var sb = AssetDatabase.LoadAssetAtPath<ScriptableBase>(datapath + $"{filename}.asset");
                var lines = File.ReadAllLines(path);
                sb.Convert(lines);
            }
            
        }
    }
    static bool Check(string path)
    {
        //指定フォルダー
        if (!path.Contains(datapath))
        {
            return false;
        }
        //拡張子がcsv
        var ext = Path.GetExtension(path);
        if(ext != ".csv")
        {
            return false;
        }
        return true;
    }

    static void CreateSource(string path)
    {
        var filename = Path.GetFileNameWithoutExtension(path);
        Directory.CreateDirectory(sourcePath);
        StreamWriter sw = new StreamWriter(Path.Combine(sourcePath,filename) + ".cs", false, Encoding.UTF8);

        sw.WriteLine(string.Format(templateHeader, filename));
        
        var lines = File.ReadAllLines(path);
        //enum　ID書き出し
        WriteEnum(sw, lines);

        //クラス宣言
        sw.WriteLine("[System.Serializable]\npublic class Data");
        sw.WriteLine("{");
        //型　変数　書き出し
        WriteTypeValue(sw, lines);

        //コンストラクタ
        WriteConstructor(sw, lines);
        //クラス宣言終わり
        sw.WriteLine("}");

        //リスト宣言
        sw.WriteLine("[SerializeField]\nList<Data> data = new List<Data>();");

        //CSVからのコンバートプログラム
        WriteConvert(sw, lines);
        //アクセッサ

        sw.WriteLine("}");
        sw.Close();
        AssetDatabase.Refresh();
    }

    static void WriteEnum(StreamWriter sw, string[] lines)
    {
        sw.WriteLine($"public enum ID{System.Environment.NewLine}{{");
        for (int i = 2; i < lines.Length; i++)
        {
            var splits = lines[i].Split(',');
            sw.WriteLine(splits[1] + ",");
        }
        sw.WriteLine("}");
    }

    static void WriteTypeValue(StreamWriter sw, string[] lines)
    {
        var t = lines[0].Split(',');
        var v = lines[1].Split(',');
        for(int i = 2; i < t.Length; i++)
        {
            sw.WriteLine("[SerializeField]");
            sw.WriteLine($"{t[i]} {v[i]};");
        }
    }
    static void WriteConstructor(StreamWriter sw, string[] lines)
    {
        sw.WriteLine($"public Data(");
        var t = lines[0].Split(',');
        var v = lines[1].Split(',');
        for (int i = 2; i < t.Length; i++)
        {
            sw.Write($"{t[i]} {v[i]}");
            if (i != t.Length - 1)
            {
                sw.Write(',');
            }
            else
            {
                sw.Write(')');
            }
        }
        sw.WriteLine("{");
        for (int i = 2; i < t.Length; i++)
        {
            sw.WriteLine($"this.{v[i]} = {v[i]};");
        }
        sw.WriteLine("}");
    }
    static void WriteList(StreamWriter sw)
    {

    }

    static void WriteConvert(StreamWriter sw, string[] lines)
    {
        var t = lines[0].Split(',');
        sw.WriteLine("public override void Convert(string[] lines){");
        for(int i = 2; i < lines.Length; i++)
        {
            var s = lines[i].Split(',');
            sw.Write("data.Add(new Data(");
            for (int j = 2; j < s.Length; j++) {
                var v = s[j];
                if(t[j] == "string")
                {
                    v = "\"" + v + "\"";
                }
                sw.Write($"{v}");
                if (j != s.Length - 1) sw.Write(", ");
            }
            sw.WriteLine("));");
            
        }
        sw.WriteLine("}");
    }
}
