using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace ArtOfRallySuiVR.Hacks
{
	[HarmonyPatch]
	public class StageIntroCinematicHack
	{
#pragma warning disable CS0618 // Type or member is obsolete
		[HarmonyPostfix]
		[HarmonyPatch(typeof(StageIntroCinematic), "FinishCinematic")]
		public static void StageIntroCinematicFinishCinematicPostfix()
		{
			UnityEngine.XR.InputTracking.Recenter();
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(StageIntroCinematic), "SetStartingPositions")]
		public static bool StageIntroCinematicSetStartingPositions(StageIntroCinematic __instance, Text ___FirstText, Text ___SecondText)
		{
			var findStartingTransforms = AccessTools.Method(typeof(StageIntroCinematic), "FindStartingTransforms");
			findStartingTransforms.Invoke(__instance, new object[] { });
			LeanTween.alphaText(___FirstText.rectTransform, 0f, 0f);
			LeanTween.alphaText(___SecondText.rectTransform, 0f, 0f);
			return false;
		}

		[HarmonyPostfix]
		[HarmonyPatch(typeof(StageIntroCinematic), "SetStartingPositions")]
		public static void StageIntroCinematicSetStartingPositionsPostfix(RawImage ___DividingLine, Transform ___cameraTransform, Transform ___StartingTransform)
		{
			___DividingLine.rectTransform.localScale = new Vector3(___DividingLine.rectTransform.localScale.x, 0f, ___DividingLine.rectTransform.localScale.z);
			___cameraTransform.position = ___StartingTransform.position;
			___cameraTransform.rotation = ___StartingTransform.rotation;
			UnityEngine.XR.InputTracking.Recenter();
		}
#pragma warning restore CS0618 // Type or member is obsolete

	}
}
