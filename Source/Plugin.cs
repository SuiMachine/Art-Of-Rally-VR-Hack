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
			Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
			loggerInstance = Logger;
			harmonyInstance = new HarmonyLib.Harmony("local.artofrallysuivr.suicidemachine");

			if (UnityEngine.XR.XRSettings.loadedDeviceName != "")
			{
#pragma warning disable CS0618 // Type or member is obsolete
				Logger.LogInfo($"Initializing VR for {UnityEngine.XR.XRSettings.loadedDeviceName}");
				UnityEngine.XR.InputTracking.disablePositionalTracking = true;
				GlobalVRInstancesManager.Init();
				ProcessConfig();

				Logger.LogInfo($"Hotpatching...");
				harmonyInstance.PatchAll();
#pragma warning restore CS0618 // Type or member is obsolete

				Logger.LogInfo($"Seems to be OK");
			}
			else
				Logger.LogInfo($"Loaded device is None - ignoring hotpatching.");
		}

		private void ProcessConfig()
		{
			Hacks.HookPoints.LowCamerasDetour.Use = Config.Bind("Modifications", "EnableLowCam", false).Value;
		}
	}
}
