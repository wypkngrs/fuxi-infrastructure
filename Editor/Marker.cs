using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class Marker
{
    static void ShowLabel(Rect rect, string label, Color color, bool overwrite = true, float offset = 30)
    {
        var style = new GUIStyle();
        style.normal.textColor = color;
        style.fontSize = 10;
        if (overwrite)
        {
            GUI.Box(rect, "");
            GUI.Label(rect, label, style);
        }
        else if(s_BiggestRect != null)
        {
            Rect r = new Rect(s_BiggestRect);
            var width = 80;
            float dis = (s_BiggestRect.width - rect.width);
            r.x = s_BiggestRect.width - width + offset;
            r.y = rect.y;
            r.width = width;
            GUI.Label(r, label, style);
        }
    }

    static Rect s_BiggestRect;

    static Marker()
    {
        string[] DONT_MODIFY_LIST = new string[]
        {
            "Assets/ThirdParty/Infrastructure",
            "Assets/Builds"
        };

        string[] DONT_SUBMIT_LIST = new string[]
        {
            "Assets/Temp"
        };

        List<string> SHOW_LIST = new List<string>(new string[]
        {
            "Look Dev",
            "Prefabs"
        });

        EditorApplication.projectWindowItemOnGUI = (guid, rect) =>
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);

            if (path == "Assets")
            {
                s_BiggestRect = rect;
            }

#if !FUXI_SUPERVISOR
            if (path.Contains("Assets/AddressableAssetsData"))
            {
                ShowLabel(rect, "[系统文件，请勿修改]", Color.black);
                return;
            }
#endif
            if (path.Contains("Prefabs"))
            {
                ShowLabel(rect, "[预制体]", Color.black, false);
                return;
            }
            if (path.Contains("Look Dev"))
            {
                ShowLabel(rect, "[Look Dev场景]", Color.black, false, 0);
                return;
            }
            if (path == "Assets/Resources")
            {
                ShowLabel(rect, "[通用文件]", Color.black, false, 0);
                return;
            }
            if (path.Contains("Resources/Shaders"))
            {
                ShowLabel(rect, "[自定义Shader]", Color.black, false, 0);
                return;
            }
            if (path.Contains("Plugins"))
            {
                ShowLabel(rect, "[小型插件]", Color.black, false, 0);
                return;
            }
            if (path.Contains("ThirdParty"))
            {
                ShowLabel(rect, "[大型第三方库]", Color.black, false, 0);
                return;
            }
            foreach (var s in DONT_SUBMIT_LIST)
            {
                if (path == s)
                {
                    ShowLabel(rect, "[临时文件存放处]", Color.black, false, 0);
                    return;
                }
                else if (path.Contains(s))
                {
                    ShowLabel(rect, "[请勿提交]", Color.red, false, 0);
                    return;
                }
            }
#if FUXI_DEVELOPER
            foreach (var s in DONT_MODIFY_LIST)
            {
                if (path == s)
                {
                    ShowLabel(rect, "[美术仓库]", Color.black, false, 0);
                    return;
                }
                else if (path.Contains(s))
                {
                    bool exclusive = true;
                    foreach (var l in SHOW_LIST)
                    {
                        if (path.Contains(l))
                        {
                            exclusive = false;
                        }
                    }
                    if (exclusive)
                    {
                        ShowLabel(rect, "[美术资源，请勿修改]", Color.black);
                    }
                    return;
                }
            }
            if (path.Contains("Assets/Scripts"))
            {
                ShowLabel(rect, "[业务逻辑代码]", Color.black, false, 0);
                return;
            }
            
#elif FUXI_ARTIST
            if (path == "Assets/Builds")
            {
                ShowLabel(rect, "[美术仓库]", Color.black, false, 0);
                return;
            }
            if (path.Contains("Meshes"))
            {
                ShowLabel(rect, "[模型]", Color.black,false);
                return;
            }
            if (path.Contains("Materials"))
            {
                ShowLabel(rect, "[材质]", Color.black,false);
                return;
            }
            if (path.Contains("Animations"))
            {
                ShowLabel(rect, "[动画]", Color.black,false);
                return;
            }
            if (path.Contains("Textures"))
            {
                ShowLabel(rect, "[贴图]", Color.black,false);
                return;
            }
            if (path.Contains("Miscellaneous"))
            {
                ShowLabel(rect, "[杂项]", Color.black, false);
                return;
            }
            if (path.Contains("Assets/Scripts"))
            {
                ShowLabel(rect, "[代码文件，请勿修改]", Color.black);
                return;
            }
#endif
        };
    }
}
