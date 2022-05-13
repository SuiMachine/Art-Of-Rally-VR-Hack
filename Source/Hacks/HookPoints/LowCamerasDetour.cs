using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace ArtOfRallySuiVR.Hacks.HookPoints
{
	/// <summary>
	/// This replaces some of the cameras, with cameras really close to the car
	/// </summary>
	[HarmonyPatch]
	public class LowCamerasDetour
	{
		public static bool Use;
		private static int CachedLayerMask;

		[HarmonyPostfix]
		[HarmonyPatch(typeof(CarCameras), "Start")]
		public static void StartDetour(List<CameraAngle> ___CameraAnglesList)
		{
			if(Use)
			{
				___CameraAnglesList[(int)CameraAngle.CameraAngles.CAMERA1] = new CameraAngle(8, 3f, -4, CameraAngle.CameraAngles.CAMERA1);
				___CameraAnglesList[(int)CameraAngle.CameraAngles.CAMERA2] = new CameraAngle(8, 2.5f, -3, CameraAngle.CameraAngles.CAMERA2);
				___CameraAnglesList[(int)CameraAngle.CameraAngles.CAMERA3] = new CameraAngle(8, 2f, -2, CameraAngle.CameraAngles.CAMERA3);
				___CameraAnglesList[(int)CameraAngle.CameraAngles.CAMERA4] = new CameraAngle(5.5f, 3.5f, -4, CameraAngle.CameraAngles.CAMERA4);
				___CameraAnglesList[(int)CameraAngle.CameraAngles.CAMERA5] = new CameraAngle(5.5f, 3f, -4, CameraAngle.CameraAngles.CAMERA5);
				___CameraAnglesList[(int)CameraAngle.CameraAngles.CAMERA6] = new CameraAngle(5.5f, 2.5f, -3, CameraAngle.CameraAngles.CAMERA6);
				___CameraAnglesList[(int)CameraAngle.CameraAngles.CAMERA7] = new CameraAngle(4, 3f, -2, CameraAngle.CameraAngles.CAMERA7);
				___CameraAnglesList[(int)CameraAngle.CameraAngles.CAMERA8] = new CameraAngle(4, 2.5f, -2, CameraAngle.CameraAngles.CAMERA8);
				CachedLayerMask = LayerMask.GetMask("Terrain");
			}
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(CarCameras), "Update")]
		public static void UpdateDetour(Transform ___target, Transform ___myTransform, Transform ___yawPointer, float ___height)
		{
			if(Use)
			{
				if(___target && (GameEntryPoint.EventManager.status == EventStatusEnums.EventStatus.UNDERWAY || GameEntryPoint.EventManager.status == EventStatusEnums.EventStatus.IN_PRE_STAGE_SCREEN || GameEntryPoint.EventManager.status == EventStatusEnums.EventStatus.WAITING_TO_BEGIN))
				{
					if(Physics.Raycast(___myTransform.transform.position + Vector3.up * 20, Vector3.down, out var hit, 40, CachedLayerMask))
					{
						___myTransform.transform.position = hit.point + Vector3.up * ___height;
					}

					if (Physics.Raycast(___yawPointer.transform.position + Vector3.up * 20, Vector3.down, out var hit2, 40, CachedLayerMask))
					{
						___yawPointer.transform.position = hit2.point + Vector3.up * ___height;
					}
				}
			}
		}
	}
}
