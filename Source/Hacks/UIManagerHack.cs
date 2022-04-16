using HarmonyLib;
using UnityEngine;

namespace ArtOfRallySuiVR.Hacks
{
	[HarmonyPatch]
	public class UIManagerHack
	{
		[HarmonyPostfix]
		[HarmonyPatch(typeof(UIManager), "Awake")]
		public static void UIManagerPostfix(UIManager __instance)
		{
			if (__instance.gameObject.scene.name == Universal.ConstSceneNames.MainMenu)
			{
				if (!__instance.GetComponent<VRMainMenuBehaviour>())
				{
					__instance.gameObject.AddComponent<VRMainMenuBehaviour>();
				}
			}
			else
			{
				if (!__instance.GetComponent<VRInGameUIBehaviour>())
				{
					__instance.gameObject.AddComponent<VRInGameUIBehaviour>();
				}
			}
		}
	}
}
