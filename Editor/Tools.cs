using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tools
{
    [MenuItem("Tools/Fuxi/Mode/Switch To Developer")]
    static void SwitchToDevelopers()
    {
        var sb = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
        sb = sb.Replace(";FUXI_ARTIST", "");
        sb = sb.Replace(";FUXI_DEVELOPER", "");
        sb = sb.Replace(";FUXI_SUPERVISOR", "");
        sb = sb.Replace("FUXI_ARTIST", "");
        sb = sb.Replace("FUXI_DEVELOPER", "");
        sb = sb.Replace("FUXI_SUPERVISOR", "");
        sb = sb + ";FUXI_DEVELOPER";
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, sb);
    }

    [MenuItem("Tools/Fuxi/Mode/Switch To Artist")]
    static void SwitchToArtists()
    {
        var sb = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
        sb = sb.Replace(";FUXI_ARTIST", "");
        sb = sb.Replace(";FUXI_DEVELOPER", "");
        sb = sb.Replace(";FUXI_SUPERVISOR", "");
        sb = sb.Replace("FUXI_ARTIST", "");
        sb = sb.Replace("FUXI_DEVELOPER", "");
        sb = sb.Replace("FUXI_SUPERVISOR", "");
        sb = sb + ";FUXI_ARTIST";
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, sb);
    }

    [MenuItem("Tools/Fuxi/Mode/Switch To Supervisor")]
    static void SwitchToSupervisor()
    {
        var sb = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
        sb = sb.Replace(";FUXI_ARTIST", "");
        sb = sb.Replace(";FUXI_DEVELOPER", "");
        sb = sb.Replace(";FUXI_SUPERVISOR", "");
        sb = sb.Replace("FUXI_ARTIST", "");
        sb = sb.Replace("FUXI_DEVELOPER", "");
        sb = sb.Replace("FUXI_SUPERVISOR", "");
        sb = sb + ";FUXI_SUPERVISOR";
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, sb);
    }
}
