using UnityEngine.AssetGraph;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;
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

public class AASUtility : UnityEditor.Editor
{

    const string addressableAssetSettings = "Assets/AddressableAssetsData/AddressableAssetSettings.asset";
    const string analyzeRuleData = "Assets/AddressableAssetsData/AnalyzeData/AnalyzeRuleData.asset";

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
            e.SetAddress(address);
        }
    }

    /// <summary>
    /// 空グループを削除
    /// </summary>
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
    /// 文字列順にソートする
    /// </summary>
    static public void Sort()
    {
        var s = GetSettings();
        s.groups.Sort(new GroupCompare());
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
            UnityEditor.AddressableAssets.Settings.GroupSchemas.BundledAssetGroupSchema.CreateInstance<UnityEditor.AddressableAssets.Settings.AddressableAssetGroupSchema>(),
            UnityEditor.AddressableAssets.Settings.GroupSchemas.ContentUpdateGroupSchema.CreateInstance<UnityEditor.AddressableAssets.Settings.AddressableAssetGroupSchema>()
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
    /// <param name="assetGuidList"></param>
    /// <param name="label"></param>
    /// <param name="flag"></param>
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

    /// <summary>
    /// アドレサブルをビルド
    /// </summary>
    static void BuildPlayerContent()
    {
        var d = GetSettings();
        UnityEditor.AddressableAssets.Settings.AddressableAssetSettings.BuildPlayerContent();
    }
    [UnityEditor.MenuItem("test/run")]
    static public void Test()
    {
        var d = GetSettings();

        var matguid = UnityEditor.AssetDatabase.AssetPathToGUID("Assets/Data/hogeMat.mat");
        AddAssetToGroup(matguid, "CreatedGroup");
        ////List<string> assetGuidList = new List<string>() { matguid };
        ////SetLabelToAsset(assetGuidList, "mat", true);
        //CreateGroup("CreatedGroup");
    }


    [MenuItem("test/ExecuteGraph")]
    static public void ExecuteGraph()
    {
        AssetGraphUtility.ExecuteGraph("Assets/AssetGraph/Graph/Stage.asset");
    }
}
