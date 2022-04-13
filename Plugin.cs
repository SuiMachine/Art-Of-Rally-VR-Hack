using ArtOfRallySuiVR.Hacks;
using BepInEx;
using BepInEx.Configuration;

namespace ArtOfRallySuiVR
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static HarmonyLib.Harmony harmonyInstance;
        public static BepInEx.Logging.ManualLogSource loggerInstance;
        private readonly ConfigEntry<bool> ConfigInitializeVR;

        public Plugin()
		{
            ConfigInitializeVR = Config.Bind("General", "InitializeVR", true);
		}

        private void Awake()
        {
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            loggerInstance = Logger;

            if (ConfigInitializeVR.Value)
			{
#pragma warning disable CS0618 // Type or member is obsolete
                Logger.LogInfo($"Initializing VR using {UnityEngine.XR.XRSettings.supportedDevices[1]}");
                UnityEngine.XR.XRSettings.enabled = true;
                UnityEngine.XR.XRSettings.LoadDeviceByName(UnityEngine.XR.XRSettings.supportedDevices[1]);
                UnityEngine.XR.XRDevice.SetTrackingSpaceType(UnityEngine.XR.TrackingSpaceType.Stationary);
                UnityEngine.XR.InputTracking.disablePositionalTracking = true;
                harmonyInstance = new HarmonyLib.Harmony("local.artofrallysuivr.suicidemachine");
#pragma warning restore CS0618 // Type or member is obsolete

                GlobalVRInstancesManager.Init();
                Logger.LogInfo($"Seems to be OK");
            }
        }
    }
}
