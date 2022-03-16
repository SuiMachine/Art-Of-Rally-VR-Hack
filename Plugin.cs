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
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            loggerInstance = Logger;
            harmonyInstance = new HarmonyLib.Harmony("local.artofrallysuivr.suicidemachine");
            VRCameraAdditionalController.Init();

            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} initialized!");

        }
    }
}
