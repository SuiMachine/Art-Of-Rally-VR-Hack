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
#pragma warning disable CS0618 // Type or member is obsolete
			UnityEngine.XR.InputTracking.disablePositionalTracking = true;
#pragma warning restore CS0618 // Type or member is obsolete

			if (__instance.gameObject.scene.name == Universal.ConstSceneNames.MainMenu)
			{
				foreach (var vrRecenter in VR_Recenter.VRCameraInstances)
				{
					vrRecenter.enabled = false;
					vrRecenter.SelfDestroy();
				}
			}
			else
			{
				if (__instance.GetComponent<VR_Recenter>() == null && __instance.GetComponent<Camera>() != null)
				{
					__instance.gameObject.AddComponent<VR_Recenter>();
				}
			}
		}
	}
}
