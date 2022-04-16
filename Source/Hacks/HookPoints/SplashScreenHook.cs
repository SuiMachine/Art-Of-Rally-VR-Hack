using HarmonyLib;

namespace ArtOfRallySuiVR.Hacks.HookPoints
{
	[HarmonyPatch]
	public class SplashScreenHook
	{
		[HarmonyPostfix]
		[HarmonyPatch(typeof(SplashScreenControl), nameof(SplashScreenControl.RunSplashScreen))]
		public static void RunSplashScreenPostfix()
		{
			VRMainMenuBehaviour.RepositionMenu();
		}
	}
}
