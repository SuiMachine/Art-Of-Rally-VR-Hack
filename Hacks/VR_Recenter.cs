using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArtOfRallySuiVR.Hacks
{
	public class VR_Recenter : MonoBehaviour
	{
#pragma warning disable CS0618 // Type or member is obsolete
		public static List<VR_Recenter> VRCameraInstances = new List<VR_Recenter>();
		Camera cameraRef;
		Transform reorientNodeTransform;

		void Start()
		{
			cameraRef = this.GetComponent<Camera>();
			if(!this.transform.parent.name.StartsWith("VR"))
			{
				UnityEngine.XR.InputTracking.disablePositionalTracking = true;
				var reorientNode = new GameObject("VRReorient");
				reorientNode.transform.SetParent(this.transform.parent, true);
				reorientNode.transform.localPosition = this.transform.localPosition;
				reorientNode.transform.localRotation = this.transform.localRotation;
				this.reorientNodeTransform = reorientNode.transform;
				this.transform.SetParent(reorientNodeTransform.transform, true);
				VRCameraInstances.Add(this);
				SetForward();
				if(AwesomeTechnologies.VegetationStudio.VegetationStudioManager.Instance != null)
				{
					var vegeSystems = AwesomeTechnologies.VegetationStudio.VegetationStudioManager.Instance.VegetationSystemList;
					foreach(var vegeSystem in vegeSystems)
					{
						foreach(var vegeCam in vegeSystem.VegetationStudioCameraList)
						{
							vegeCam.CameraCullingMode = AwesomeTechnologies.VegetationSystem.CameraCullingMode.Complete360;
						}
					}
				}
			}
		}

		public void SetForward()
		{
			reorientNodeTransform.localEulerAngles = new Vector3(0, -this.transform.localEulerAngles.y, 0);
		}

		void OnDestroy()
		{
			if (VRCameraInstances.Contains(this))
				VRCameraInstances.Remove(this);
		}
#pragma warning restore CS0618 // Type or member is obsolete
	}
}
