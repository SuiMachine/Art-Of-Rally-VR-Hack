using HarmonyLib;
using UnityEngine;

namespace ArtOfRallySuiVR.Hacks.HookPoints
{
#pragma warning disable CS0618 // Type or member is obsolete
	[HarmonyPatch]
	public class CameraMoverMainMenuDetour
	{
		[HarmonyPrefix]
		[HarmonyPatch(typeof(CameraMoverMainMenu), nameof(CameraMoverMainMenu.ZoomIn))]
		public static bool ZoomIn(CameraMoverMainMenu __instance, ref Camera ___TheCamera)
		{
			if (___TheCamera == null)
			{
				var initMethod = AccessTools.Method(typeof(CameraMoverMainMenu), "Init");
				initMethod.Invoke(__instance, new object[] { });
			}
			var carCameraTransform = (Transform)__instance.GetType().GetField("CarCameraTargetTransform", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(__instance);
			__instance.LerpCameraToCarPosition(carCameraTransform);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(CameraMoverMainMenu), nameof(CameraMoverMainMenu.ZoomOut))]
		public static bool ZoomOut(CameraMoverMainMenu __instance,  bool showDiorama)
		{
			if (showDiorama)
			{
				UIManager.Instance.PanelManager.DioramaManager.EnableDefaultDiorama();
			}
			else
			{
				UIManager.Instance.PanelManager.DioramaManager.DisableAllDioramas();
			}
			return false;
		}


		[HarmonyPrefix]
		[HarmonyPatch(typeof(CameraMoverMainMenu), nameof(CameraMoverMainMenu.MoveCameraToCareerPosition))]
		public static bool MoveCameraToCareerPosition(CameraMoverMainMenu __instance, Transform ___CareerTransform)
		{
			__instance.transform.parent.parent.position = ___CareerTransform.position;
			__instance.transform.parent.parent.eulerAngles = new Vector3(0, ___CareerTransform.eulerAngles.y, 0);
			VRMainMenuBehaviour.SetInFront(__instance.transform.position, __instance.transform.rotation, 5f);
			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(CameraMoverMainMenu), nameof(CameraMoverMainMenu.ResetToStartPosition))]
		public static bool ResetToStartPosition(bool showDiorama, float animationTime = 0.5f)
		{
			if(showDiorama)
			{
				UIManager.Instance.PanelManager.DioramaManager.EnableDefaultDiorama();
			}
			else
			{
				UIManager.Instance.PanelManager.DioramaManager.DisableAllDioramas();
			}
			return false;
		}


		[HarmonyPrefix]
		[HarmonyPatch(typeof(CameraMoverMainMenu), nameof(CameraMoverMainMenu.LerpCameraToCarPosition))]
		public static bool LerpCameraToCarPosition(CameraMoverMainMenu __instance, Transform ___CarCameraTargetTransform, Transform CarPosition)
		{
			__instance.transform.parent.parent.position = ___CarCameraTargetTransform.position;
			__instance.transform.parent.parent.eulerAngles = new Vector3(0, ___CarCameraTargetTransform.eulerAngles.y, 0);
			VRMainMenuBehaviour.SetInFront(__instance.transform.position, __instance.transform.rotation, 5f);

			return false;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(CameraMoverMainMenu), nameof(CameraMoverMainMenu.LerpCameraToRallyCompleteCar))]
		public static bool LerpCameraToRallyCompleteCar(CameraMoverMainMenu __instance, Transform ___RallyCompleteCameraTarget)
		{
			__instance.transform.parent.parent.position = ___RallyCompleteCameraTarget.position;
			__instance.transform.parent.parent.eulerAngles = new Vector3(0, ___RallyCompleteCameraTarget.eulerAngles.y, 0);
			VRMainMenuBehaviour.SetInFront(__instance.transform.position, __instance.transform.rotation, 5f);

			return false;
		}
#pragma warning restore CS0618 // Type or member is obsolete
	}
}
