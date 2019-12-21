using UnityEngine.AssetGraph;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;
using System.Linq;

public class AASUtility : UnityEditor.Editor
{
    static public string assetRoot = "Assets" + System.IO.Path.DirectorySeparatorChar + "Data" + System.IO.Path.DirectorySeparatorChar;
    const string addressableAssetSettings = "Assets/AddressableAssetsData/AddressableAssetSettings.asset";
    const string menuName = "AASUtility";

    static string editorDir = "Editor";
    const string dataRoot = "Assets/Data/";
    /// <summary>
    /// データディレクトリのルートから全グループを作成する
    /// </summary>
    [MenuItem(menuName+"/CreateAssetsDataGroup")]
    static public void CreateGroup()
    {
        var assetPaths = AssetDatabase.GetAllAssetPaths().ToList();
        var dataPath = assetPaths.FindAll(p => p.Contains(dataRoot));
        foreach (var asset in dataPath)
        {
            AddGroup(asset);
        }
        Sort();
    }

    static public bool AddGroup(string asset)
    {
        var dir = System.IO.Path.GetDirectoryName(asset);

        //グループに含めていいかのチェック
        //ディレクトリとエディターディレクトリのアセットは含めない
        if (System.IO.Directory.Exists(asset)) return false;
        if (dir.Contains(editorDir)) return false;

        //アドレス名を求める計算
        var filename = System.IO.Path.GetFileName(asset);
        dir = dir.Replace(assetRoot, "");
        string group = dir.Replace(System.IO.Path.DirectorySeparatorChar, '_');
        var address = group + "_" + System.IO.Path.GetFileNameWithoutExtension(asset);

        AASUtility.AddAssetToGroup(AssetDatabase.AssetPathToGUID(asset), group, address);
        return true;
    }

    /// <summary>
    /// 任意のアセットをグループに追加
    /// </summary>
    /// <param name="assetGuid"></param>
    /// <param name="groupName"></param>
    /// <param name="address"></param>
    static public void AddAssetToGroup(string assetGuid, string groupName, string address = null)
    {
        var s = GetSettings();
        var g = CreateGroup(groupName);
        var e = s.CreateOrMoveEntry(assetGuid, g);
        if(address != null)
        {
            List<AddressableAssetEntry> entries = new List<AddressableAssetEntry>();
            s.GetAllAssets(entries, true);
            if (CheckAddress(entries, address))
            {
                e.SetAddress(address);
            }
            else
            {
                Debug.LogAssertion("Duplicate Address");
            }
        }
    }

    /// <summary>
    /// 文字列順にソートする
    /// </summary>
    [MenuItem(menuName + "/Sort")]
    static public void Sort()
    {
        var s = GetSettings();
        s.groups.Sort(new GroupCompare());
    }

    /// <summary>
    /// 空グループを削除
    /// </summary>
    [MenuItem(menuName+"/Remove/EmptyGroup")]
    static public void DeleteEmptyGroup()
    {
        var s = GetSettings();
        var groups = s.groups;
        foreach (var g in groups)
        {
            if (g.entries.Count == 0 && !g.IsDefaultGroup())
            {
                s.RemoveGroup(g);
            }   
        }
    }

    /// <summary>
    /// 全グループを削除
    /// </summary>
    [MenuItem(menuName+"/Remove/AllGroup")]
    static public void RemoveAllGroup()
    {
        var s = GetSettings();
        var groups = s.groups;
        for(int i = groups.Count - 1; i >= 0; i--)
        {
            if (groups[i].IsDefaultGroup()) continue;
            s.RemoveGroup(groups[i]);
        }

    }
    /// <summary>
    /// 全アドレスの重複チェック
    /// 重複しているとビルドできない
    /// </summary>
    [MenuItem(menuName + "/CheckAllAddress")]
    static public void CheckAllAddress()
    {
        var s = GetSettings();
        List<AddressableAssetEntry> entries = new List<AddressableAssetEntry>();
        s.GetAllAssets(entries, true);
        List<string> checkedAddress = new List<string>();
        foreach (var e in entries)
        {
            //チェック済みアドレスはコンテニュー
            if (checkedAddress.Contains(e.address)) continue;

            //全アセットで重複があるかチェック
            bool ret = CheckAddress(entries, e.address);
            if (!ret)
            {
                checkedAddress.Add(e.address);
            }
        }
    }

    /// <summary>
    /// アドレサブルをビルド
    /// </summary>
    [MenuItem(menuName + "/Build")]
    static public void Build()
    {
        AddressableAssetSettings.BuildPlayerContent();
    }

    /// <summary>
    /// ビルドをクリーン
    /// </summary>
    [MenuItem(menuName + "/CleanBuild")]
    static public void Clean()
    {
        AddressableAssetSettings.CleanPlayerContent();
    }

    /// <summary>
    /// 重複アドレスチェック
    /// </summary>
    /// <param name="entries"></param>
    /// <param name="address"></param>
    /// <returns>重複なしtrue、ありfalse</returns>
    static public bool CheckAddress(List<AddressableAssetEntry> entries, string address)
    {
        var s = GetSettings();
        var duplicateEntries = entries.FindAll(e=>e.address == address);
        if(duplicateEntries.Count > 1)
        {
            string str = "Address=" + address + System.Environment.NewLine;
            foreach (var e in duplicateEntries)
            {
                string assetname = System.IO.Path.GetFileName(e.AssetPath);
                str += "Group=" + e.parentGroup.Name + "," + "AssetName=" + assetname + System.Environment.NewLine;
            }
            Debug.LogAssertion("DuplicateAddress" + System.Environment.NewLine + str);
            return false;
        }
        return true;
    }

    
    static UnityEditor.AddressableAssets.Settings.AddressableAssetSettings GetSettings()
    {
        //アドレサブルアセットセッティング取得
        var d = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEditor.AddressableAssets.Settings.AddressableAssetSettings>(
            addressableAssetSettings
            );
        return d;
    }

    
    /// <summary>
    /// グループを作成
    /// </summary>
    /// <param name="groupName"></param>
    /// <returns></returns>
    static UnityEditor.AddressableAssets.Settings.AddressableAssetGroup CreateGroup(string groupName)
    {
        //アドレサブルアセットセッティング取得
        var s = GetSettings();
        //スキーマ生成
        List<UnityEditor.AddressableAssets.Settings.AddressableAssetGroupSchema> schema = new List<UnityEditor.AddressableAssets.Settings.AddressableAssetGroupSchema>() {
            UnityEditor.AddressableAssets.Settings.GroupSchemas.BundledAssetGroupSchema.CreateInstance<UnityEditor.AddressableAssets.Settings.GroupSchemas.BundledAssetGroupSchema>(),
            UnityEditor.AddressableAssets.Settings.GroupSchemas.ContentUpdateGroupSchema.CreateInstance<UnityEditor.AddressableAssets.Settings.GroupSchemas.ContentUpdateGroupSchema>()
        };
        //グループの作成
        var f = s.groups.Find((g) => { return g.name == groupName; });
        if (f == null)
        {
            return s.CreateGroup(groupName, false, false, true, schema);
        }

        return f;
    }

    /// <summary>
    /// アセットにラベルを一括設定
    /// </summary>
    /// <param name="assetGuidList">対象アセットのGUIDリスト</param>
    /// <param name="label">ラベル名</param>
    /// <param name="flag">ラベル有効、無効のフラグ</param>
    static void SetLabelToAsset(List<string> assetGuidList, string label, bool flag)
    {
        var s = GetSettings();
        //ラベルを追加するように呼んでおく。追加されていないと設定されない。
        s.AddLabel(label);
        List<UnityEditor.AddressableAssets.Settings.AddressableAssetEntry> assetList = new List<UnityEditor.AddressableAssets.Settings.AddressableAssetEntry>();
        s.GetAllAssets(assetList, true);
        foreach (var assetGuid in assetGuidList)
        {
            var asset = assetList.Find((a) => { return a.guid == assetGuid; });
            if(asset != null)
            {
                asset.SetLabel(label, flag);
            }
        }
    }

    /// <summary>
    /// グループからアセットを削除
    /// </summary>
    /// <param name="assetGuid"></param>
    static void RemoveAssetFromGroup(string assetGuid)
    {
        var s = GetSettings();
        s.RemoveAssetEntry(assetGuid);
    }
    

    [MenuItem("test/ExecuteGraph")]
    static public void ExecuteGraph()
    {
        AssetGraphUtility.ExecuteGraph("Assets/AssetGraph/Graph/Stage.asset");
    }
}


/// <summary>
/// グループを文字列順に並べるソート
/// </summary>
class GroupCompare : Comparer<UnityEditor.AddressableAssets.Settings.AddressableAssetGroup>
{
    public override int Compare(UnityEditor.AddressableAssets.Settings.AddressableAssetGroup x, UnityEditor.AddressableAssets.Settings.AddressableAssetGroup y)
    {
        int r = string.CompareOrdinal(x.Name, y.Name);
        if (r > 0)
        {
            return 1;
        }
        else if (r < 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}