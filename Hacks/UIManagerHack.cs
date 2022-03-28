using HarmonyLib;
using UnityEngine;

namespace ArtOfRallySuiVR.Hacks
{
	[HarmonyPatch]
	public class UIManagerHack
	{
		//[HarmonyPostfix]
		//[HarmonyPatch(typeof(UIManager), "Awake")]
		//public static void UIManagerPostfix(UIManager __instance)
		//{
		//	if(!__instance.GetComponent<VRCameraAdditionalController>())
		//	{
		//		__instance.gameObject.AddComponent<VRUIManager>();
		//	}
		//}
	}
}
