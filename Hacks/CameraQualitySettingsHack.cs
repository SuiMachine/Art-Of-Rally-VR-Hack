using HarmonyLib;
using UnityEngine;
using UnityEngine.XR;

namespace ArtOfRallySuiVR.Hacks
{
	[HarmonyPatch]
	class CameraQualitySettingsHack
	{
		[HarmonyPostfix]
		[HarmonyPatch(typeof(CameraQualitySettings), nameof(CameraQualitySettings.LoadSettings))]
		public static void LoadSettingsPostfix(HxVolumetricCamera ___HxVolumetricCamera)
		{
			if(___HxVolumetricCamera.GetComponent<VR_Recenter>() == null)
			{
				___HxVolumetricCamera.gameObject.AddComponent<VR_Recenter>();
			}
		}
	}
}
