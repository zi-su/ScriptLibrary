using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
public class CreateScriptableSource
{
    [MenuItem("Assets/CraeteSourceScriptable")]
    static public void Create()
    {
        foreach (var s in Selection.objects)
        {
            var path = AssetDatabase.GetAssetPath(s);
            if (CSVScriptable.Check(path))
            {
                Create(path);
            }
        }
    }

    

    static public void Create(string path)
    {
        var filename = Path.GetFileNameWithoutExtension(path);
        Directory.CreateDirectory(CSVScriptable.sourcePath);
        StreamWriter sw = new StreamWriter(Path.Combine(CSVScriptable.sourcePath, filename) + ".cs", false, Encoding.UTF8);

        sw.WriteLine(string.Format(CSVScriptable.templateHeader, filename));

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
        //アクセッサ
        WriteAccessor(sw);

        //CSVからのコンバートプログラム
        WriteConvert(sw, lines);


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
            sw.WriteLine(splits[2] + $" = {splits[1]}" + ",");
        }
        sw.WriteLine("}");
    }

    static void WriteTypeValue(StreamWriter sw, string[] lines)
    {
        var t = lines[0].Split(',');
        var v = lines[1].Split(',');
        for (int i = 2; i < t.Length; i++)
        {
            sw.WriteLine("[SerializeField]");
            sw.WriteLine($"internal {t[i]} {v[i]};");
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

    static void WriteAccessor(StreamWriter sw)
    {
        sw.WriteLine("public Data GetData(ID id){");
        sw.WriteLine("return data.Find(d=>d.Id == id);");
        sw.WriteLine("}");
    }

    static void WriteConvert(StreamWriter sw, string[] lines)
    {
        var t = lines[0].Split(',');
        sw.WriteLine("public override void Convert(string[] lines){");

        sw.WriteLine("data.Clear();");
        sw.WriteLine("var t = lines[0].Split(',');");
        sw.WriteLine("for(int i = 2; i < lines.Length; i++)");
        sw.WriteLine("{");
        sw.WriteLine("var v = lines[i].Split(',');");

        string s = "data.Add(new Data(";
        s += "(ID)int.Parse(v[1]),";
        for (int i = 3; i < t.Length; i++)
        {
            if (t[i] == "int")
            {
                s += "int.Parse(" + $"v[{i}])";
            }
            else if (t[i] == "float")
            {
                s += "float.Parse(" + $"v[{i}])";
            }
            else if (t[i].ToLower() == "bool")
            {
                s += "bool.Parse(" + $"v[{i}])";
            }
            else
            {
                s += $"v[{i}]";
            }
            if (i != t.Length - 1) s += ",";
        }
        s += "));";

        sw.WriteLine(s);
        sw.WriteLine("}");

        sw.WriteLine("}");
    }
}
