using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ArtOfRallySuiVR.Hacks
{
	[HarmonyPatch]
	public class CameraRestartInjections
	{
		[HarmonyPostfix]
		[HarmonyPatch(typeof(RigidbodyResetManager), nameof(RigidbodyResetManager.ResetAllBodies))]
		public static void PostVehicleRestartCameraSet()
		{
			foreach(var vrCameraRecenter in VR_Recenter.VRCameraInstances)
			{
				if(vrCameraRecenter != null)
				{
					vrCameraRecenter.SetForward();
				}
			}
		}
	}
}
