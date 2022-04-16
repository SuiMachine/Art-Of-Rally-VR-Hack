using HarmonyLib;

namespace ArtOfRallySuiVR.Hacks
{
	[HarmonyPatch]
	class CameraQualitySettingsHack
	{
		[HarmonyPostfix]
		[HarmonyPatch(typeof(HxVolumetricCamera), "Start")]
		public static void LoadSettingsPostfix(HxVolumetricCamera __instance)
		{
			__instance.gameObject.AddComponent<VR_Recenter>();
		}
	}
}
