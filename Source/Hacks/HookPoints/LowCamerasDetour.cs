using HarmonyLib;
using System.Collections.Generic;

namespace ArtOfRallySuiVR.Hacks.HookPoints
{
	/// <summary>
	/// This replaces some of the cameras, with cameras really close to the car
	/// </summary>
	[HarmonyPatch]
	public class LowCamerasDetour
	{
		public static bool Use;

		[HarmonyPostfix]
		[HarmonyPatch(typeof(CarCameras), "Start")]
		public static void StartDetour(List<CameraAngle> ___CameraAnglesList)
		{
			if(Use)
			{
				___CameraAnglesList[(int)CameraAngle.CameraAngles.CAMERA8] = new CameraAngle(8, 3, -2, CameraAngle.CameraAngles.CAMERA8);
			}
		}
	}
}
