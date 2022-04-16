using HarmonyLib;

namespace ArtOfRallySuiVR.Hacks.HookPoints
{
	[HarmonyPatch]
	public class UIManagerHack
	{
		[HarmonyPostfix]
		[HarmonyPatch(typeof(UIManager), "Awake")]
		public static void UIManagerPostfix(UIManager __instance)
		{
#pragma warning disable CS0618 // Type or member is obsolete
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
#pragma warning restore CS0618 // Type or member is obsolete

		}
	}
}
