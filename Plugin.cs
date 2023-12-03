using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace HarHarHarLC
{
    [BepInPlugin("kyryh.harharharlc", "Toreador March Jester", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {

        public static ManualLogSource Log;
        public static AssetBundle assetBundle;

        private void Awake()
        {
            Log = Logger;
            // Plugin startup logic

            Assembly assembly = Assembly.GetExecutingAssembly();


            using (Stream stream = assembly.GetManifestResourceStream("HarHarHarLC.Resources.harharharassetbundle")){
                assetBundle = AssetBundle.LoadFromStream(stream);
            }
            
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            Harmony harmony = new Harmony("kyryh.harharharlc");
            harmony.PatchAll();
            
        }
    }

    [HarmonyPatch(typeof(JesterAI), "Start")]
    class Patch1 {
        static public void Postfix(JesterAI __instance) {
            __instance.popGoesTheWeaselTheme = Plugin.assetBundle.LoadAsset<AudioClip>("Music_box.wav");
            
        }

    }

}

