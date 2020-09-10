using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AddressableAssets;
using UnityEditor.AddressableAssets;
using UnityEditor.VersionControl;

[InitializeOnLoad]
public static class Synchronous
{
    static Synchronous()
    {
#if !FUXI_SUPERVISOR
        RevertIllegalOperation();
#endif
        GetLatestBuild();
        Checkout();
        EditorApplication.projectChanged += () =>
        {
            GetLatestAddresable();
        };

        EditorApplication.quitting += () =>
        {
#if FUXI_DEVELOPER
            if (EditorUtility.DisplayDialog("版本控制警告", "提交Assets/Scripts文件夹下所有的修改吗？", "确定", "取消"))
            {
                Submit();
            }
#elif FUXI_ARTIST
            if (EditorUtility.DisplayDialog("版本控制警告", "提交Assets/Builds文件夹下所有的修改吗？", "确定", "取消"))
            {
                Submit();
            }
#endif
        };
    }

    static void RevertIllegalOperation()
    {
        AssetList assets = new AssetList();
#if FUXI_DEVELOPER
        assets.Add(new Asset("Assets/Builds/"));
#elif FUXI_ARTIST
        assets.Add(new Asset("Assets/AddressableAssetsData/"));
        assets.Add(new Asset("Assets/Scripts/"));
        assets.Add(new Asset("Assets/Plugins/"));
        assets.Add(new Asset("Assets/Resources/"));
        assets.Add(new Asset("Assets/ThirdParty/"));
#endif
        if (Provider.RevertIsValid(assets, RevertMode.Normal))
        {
            Task t = Provider.Revert(assets, RevertMode.Normal);
            t.Wait();
        }
    }

    static void Checkout()
    {
        AssetList assets = new AssetList();
#if FUXI_DEVELOPER
        assets.Add(new Asset("Assets/Scripts/"));
#elif FUXI_ARTIST
        assets.Add(new Asset("Assets/Builds/"));
#endif
        Task t = Provider.Checkout(assets, CheckoutMode.Both);
        t.Wait();
    }

    static void Submit()
    {
        var t = Provider.ChangeSets();
        t.Wait();

        AssetList assets = new AssetList();
        var log = "";
#if FUXI_DEVELOPER
        assets.Add(new Asset("Assets/Scripts/"));
        log = Application.productName +"Developer Auto Submit";
#elif FUXI_ARTIST
        assets.Add(new Asset("Assets/Builds/"));
        log = Application.productName +"Artist Auto Submit";
#endif
        Task t2 = Provider.Submit(t.changeSets[0], assets, log , false);
        t2.Wait();
    }


    [MenuItem("Tools/Fuxi/Update Builds")]
    static void GetLatestBuild()
    {
        Debug.Log("同步美术资源中");
        AssetList assets = new AssetList();
        assets.Add(new Asset("Assets/Builds/"));
        Task t = Provider.GetLatest(assets);
        t.Wait();
        Debug.Log("美术资源同步完成");
    }

    [MenuItem("Tools/Fuxi/Update Builds")]
    static void GetLatestAddresable()
    {
        var pathes = AssetDatabase.GetAllAssetPaths();
        foreach (var path in pathes)
        {
            var guid = AssetDatabase.AssetPathToGUID(path);
            if (path.Contains("Assets/Builds") && path.Contains("Prefabs"))
            {
                if (AssetDatabase.GetMainAssetTypeAtPath(path).GetType() == typeof(GameObject))
                {
                    var settings = AddressableAssetSettingsDefaultObject.GetSettings(true);
                    var entry = settings.FindAssetEntry(guid);
                    if (entry == null)
                    {
                        entry = settings.CreateOrMoveEntry(guid, settings.DefaultGroup);
                    }
                    entry.address = Path.GetFileNameWithoutExtension(path);
                    Debug.Log("Prefab Addressable同步完成");
                }
            }
        }
    }
}
