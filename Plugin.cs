using ArtOfRallySuiVR.Hacks;
using BepInEx;

namespace ArtOfRallySuiVR
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static HarmonyLib.Harmony harmonyInstance;
        public static BepInEx.Logging.ManualLogSource loggerInstance;

        private void Awake()
        {
#pragma warning disable CS0618 // Type or member is obsolete
			UnityEngine.XR.InputTracking.disablePositionalTracking = true;
#pragma warning restore CS0618 // Type or member is obsolete

			Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            loggerInstance = Logger;
            harmonyInstance = new HarmonyLib.Harmony("local.artofrallysuivr.suicidemachine");
            harmonyInstance.PatchAll();
            GlobalVRInstancesManager.Init();
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} initialized!");
        }
    }
}
