using HarmonyLib;
using UnityEngine;

namespace ArtOfRallySuiVR.Hacks
{
	[HarmonyPatch]
	class CameraQualitySettingsHack
	{
		[HarmonyPostfix]
		[HarmonyPatch(typeof(CameraQualitySettings), nameof(CameraQualitySettings.LoadSettings))]
		public static void LoadSettingsPostfix(Transform ___transform)
		{
			if(___transform.name == "MainCamera")
			{

			}
		}
	}
}
