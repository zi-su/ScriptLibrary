using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

using UnityEditor.AddressableAssets;
public class AASUtility : UnityEditor.Editor
{
    static UnityEditor.AddressableAssets.Settings.AddressableAssetSettings GetSettings()
    {
        //アドレサブルアセットセッティング取得
        var d = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEditor.AddressableAssets.Settings.AddressableAssetSettings>(
            "Assets/AddressableAssetsData/AddressableAssetSettings.asset"
            );
        return d;
    }


    static UnityEditor.AddressableAssets.Settings.AddressableAssetGroup CreateGroup(string groupName)
    {
        //アドレサブルアセットセッティング取得
        var s = GetSettings();
        //スキーマ生成
        List<UnityEditor.AddressableAssets.Settings.AddressableAssetGroupSchema> schema = new List<UnityEditor.AddressableAssets.Settings.AddressableAssetGroupSchema>() {
            new UnityEditor.AddressableAssets.Settings.GroupSchemas.BundledAssetGroupSchema(),
            new UnityEditor.AddressableAssets.Settings.GroupSchemas.ContentUpdateGroupSchema()
        };
        //グループの作成
        var f = s.groups.Find((g) => { return g.name == groupName; });
        if(f == null)
        {
            return s.CreateGroup(groupName, false, false, true, schema);
        }

        return f;
    }

    static void AddAssetToGroup(string assetGuid, string groupName)
    {
        var s = GetSettings();
        var g = CreateGroup(groupName);
        s.CreateOrMoveEntry(assetGuid, g);
    }

    static void RemoveAssetFromGroup(string assetGuid)
    {
        var s = GetSettings();
        s.RemoveAssetEntry(assetGuid);
    }

    [UnityEditor.MenuItem("test/run")]
    static public void Test()
    {
        var d = GetSettings();
        UnityEditor.AddressableAssets.Settings.AddressableAssetSettings.BuildPlayerContent();
        //var matguid = UnityEditor.AssetDatabase.AssetPathToGUID("Assets/Data/hogeMat.mat");
        //RemoveAssetFromGroup(matguid);
        //AddAssetToGroup(matguid, "Prefab");
    }
}
