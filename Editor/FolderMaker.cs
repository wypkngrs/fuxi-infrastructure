using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if FUXI_ARTIST || FUXI_SUPERVISOR
public class FolderMaker : EditorWindow
{
    readonly string[] FOLDER_ARRANGE_TYPES = new string[] { "Case By Case", "Massive Batch" };
    const string LOOKDEV_SCENE_PATH = "Packages/com.netease.fuxi.infrastructure/Look Dev Template.unity";

    int m_TypeSelected = 0;
    string m_PrefabName = "Default";

    [MenuItem("Assets/Create/Look Dev Prefab", priority = int.MinValue)]
    public static void CreatePrefabFolder()
    {
        var window = GetWindow<FolderMaker>();
        window.Show();
    }

    private void OnGUI()
    {
        m_TypeSelected = GUILayout.SelectionGrid(m_TypeSelected, FOLDER_ARRANGE_TYPES, 2);
        switch (m_TypeSelected)
        {
            case 0:
                {
                    m_PrefabName = GUILayout.TextField(m_PrefabName);

                    if (GUILayout.Button("Create Prefab Folders"))
                    {
                        var buildPath = Path.Combine("Assets", "Builds");
                        if (!AssetDatabase.IsValidFolder(buildPath))
                        {
                            AssetDatabase.CreateFolder("Assets", "Builds");
                            AssetDatabase.CopyAsset(
                                LOOKDEV_SCENE_PATH,
                                Path.Combine(buildPath, "Look Dev.unity")
                            );
                            AssetDatabase.Refresh();
                        }

                        var prefabRootPath = Path.Combine(buildPath, m_PrefabName);
                        if (!AssetDatabase.IsValidFolder(prefabRootPath))
                        {
                            AssetDatabase.CreateFolder(buildPath, m_PrefabName);
                            AssetDatabase.Refresh();

                            AssetDatabase.CreateFolder(prefabRootPath, "Animations");
                            AssetDatabase.CreateFolder(prefabRootPath, "Meshes");
                            AssetDatabase.CreateFolder(prefabRootPath, "Materials");
                            AssetDatabase.CreateFolder(prefabRootPath, "Textures");
                            AssetDatabase.CreateFolder(prefabRootPath, "Miscellaneous");
                            AssetDatabase.CreateFolder(prefabRootPath, "Prefabs");
                            AssetDatabase.Refresh();
                        }
                        else
                        {
                            Debug.LogFormat("{0} already exists", m_PrefabName);
                            return;
                        }
                    }
                    break;
                }
            case 1:
                {
                    if (GUILayout.Button("Create Folders"))
                    {
                        var buildPath = Path.Combine("Assets", "Builds");
                        if (!AssetDatabase.IsValidFolder(buildPath))
                        {
                            AssetDatabase.CreateFolder("Assets", "Builds");
                            AssetDatabase.Refresh();
                            
                            AssetDatabase.CopyAsset(
                                LOOKDEV_SCENE_PATH,
                                Path.Combine(buildPath, "Look Dev.unity")
                            );

                            AssetDatabase.CreateFolder(buildPath, "Animations");
                            AssetDatabase.CreateFolder(buildPath, "Meshes");
                            AssetDatabase.CreateFolder(buildPath, "Materials");
                            AssetDatabase.CreateFolder(buildPath, "Textures");
                            AssetDatabase.CreateFolder(buildPath, "Miscellaneous");
                            AssetDatabase.CreateFolder(buildPath, "Prefabs");
                            AssetDatabase.Refresh();
                        }
                    }
                    break;
                }
        }
    }
}
#endif
