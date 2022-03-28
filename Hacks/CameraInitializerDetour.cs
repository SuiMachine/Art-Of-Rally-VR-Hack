using HarmonyLib;
using UnityEngine;

namespace ArtOfRallySuiVR.Hacks
{
	[HarmonyPatch]
	class CameraQualitySettingsHack
	{
		[HarmonyPostfix]
		[HarmonyPatch(typeof(HxVolumetricCamera), "Start")]
		public static void LoadSettingsPostfix(HxVolumetricCamera __instance)
		{
			if(__instance.GetComponent<VR_Recenter>() == null && __instance.GetComponent<Camera>() != null)
			{
				__instance.gameObject.AddComponent<VR_Recenter>();
			}
		}
	}
}
