using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ArtOfRallySuiVR.Hacks
{
	public class VR_Recenter : MonoBehaviour
	{
#pragma warning disable CS0618 // Type or member is obsolete
		public static List<VR_Recenter> VRCameraInstances = new List<VR_Recenter>();
		Camera cameraRef;
		Transform reorientNodeTransform;
		BeautifyEffect.Beautify beautifyRef;


		void Start()
		{
			cameraRef = this.GetComponent<Camera>();
			beautifyRef = this.GetComponent<BeautifyEffect.Beautify>();
			var postProcess = this.GetComponent<PostProcessVolume>();
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
				StartCoroutine(SetForwardDelayed());
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

				if(postProcess != null)
				{
					var profile = postProcess.profile;
					profile.TryGetSettings<DepthOfField>(out DepthOfField dofSettings);

					if(dofSettings == null)
					{
						dofSettings = profile.AddSettings<DepthOfField>();
					}
					if(dofSettings != null)
					{
						dofSettings.active = true;
						dofSettings.enabled.overrideState = true;
						dofSettings.enabled.value = true;
						dofSettings.aperture.overrideState = true;
						dofSettings.aperture.value = 100;
						dofSettings.focusDistance.overrideState = true;
						dofSettings.focusDistance.value = 140;
						dofSettings.focalLength.overrideState = true;
						dofSettings.focalLength.value = 0.5f;
					}
				}
			}
		}

		internal void SelfDestroy()
		{
			Destroy(this);
		}

		void Update()
		{
			if(beautifyRef != null)
			{
				beautifyRef.depthOfField = false;
			}
		}

		public IEnumerator SetForwardDelayed()
		{
			yield return null;
			SetForward();
		}



		public void SetForward()
		{
			reorientNodeTransform.transform.localPosition = Vector3.zero;
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
