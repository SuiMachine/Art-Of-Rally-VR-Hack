using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SpatialTracking;

namespace ArtOfRallySuiVR.Hacks
{
	public class VR_Recenter : MonoBehaviour
	{
#pragma warning disable CS0618 // Type or member is obsolete
		public const string XRRigNodeName = "XRRig";
		public const string CameraOffsetNodeName = "Camera Offset";
		GameObject CameraOffsetNode;
		CameraOffset XRRigBase;
		TrackedPoseDriver trackedPoseDriver;

		BeautifyEffect.Beautify beautifyRef;
		HxVolumetricCamera hxVolumetricCamera;
		bool menu = false;


		void Start()
		{
			var cameraRef = this.GetComponent<Camera>();
			beautifyRef = this.GetComponent<BeautifyEffect.Beautify>();
			var postProcess = this.GetComponent<PostProcessVolume>();
			if(CameraOffsetNode == null)
			{
				CameraOffsetNode = new GameObject(CameraOffsetNodeName);
				var XRRigNode = new GameObject(XRRigNodeName);
				CameraOffsetNode.transform.SetParent(XRRigNode.transform);
				CameraOffsetNode.transform.localPosition = Vector3.zero;
				CameraOffsetNode.transform.localRotation = Quaternion.identity;

				XRRigNode.transform.SetParent(this.transform.parent);
				XRRigNode.transform.localPosition = this.transform.localPosition;
				XRRigNode.transform.localRotation = this.transform.localRotation;

				XRRigBase = XRRigNode.AddComponent<CameraOffset>();
				XRRigBase.cameraFloorOffsetObject = CameraOffsetNode;
				XRRigBase.trackingSpace = UnityEngine.XR.TrackingSpaceType.Stationary;
				XRRigBase.requestedTrackingMode = UserRequestedTrackingMode.Default;
				XRRigBase.cameraYOffset = 1.36f;

				this.transform.SetParent(CameraOffsetNode.transform, true);
				this.transform.localPosition = Vector3.zero;
				this.transform.localRotation = Quaternion.identity;
				this.trackedPoseDriver = this.gameObject.AddComponent<TrackedPoseDriver>();
				this.trackedPoseDriver.SetPoseSource(TrackedPoseDriver.DeviceType.GenericXRDevice, TrackedPoseDriver.TrackedPose.Center);
				this.trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
				this.trackedPoseDriver.updateType = TrackedPoseDriver.UpdateType.UpdateAndBeforeRender;

				//It seems like normally you'd be a bit too small
				XRRigNode.transform.localScale = Vector3.one * 1.1f;

				hxVolumetricCamera = GetComponent<HxVolumetricCamera>();

/*				if(postProcess != null)
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
				}*/
			}

			var camera = this.GetComponent<Camera>();
			if(camera != null)
			{
				camera.nearClipPlane = 0.02f;
			}

			if (this.gameObject.scene.name == Universal.ConstSceneNames.MainMenu)
			{
				menu = true;
				VRMainMenuBehaviour.RegisterCamera(this);
				UnityEngine.XR.InputTracking.Recenter();
				this.XRRigBase.transform.eulerAngles = new Vector3(0, this.XRRigBase.transform.eulerAngles.y, 0);
				VRMainMenuBehaviour.RepositionMenu();
			}
		}

		internal void SelfDestroy()
		{
			Destroy(this);
		}

		void Update()
		{
			if (beautifyRef != null)
			{
				beautifyRef.depthOfField = false;
			}
			if(hxVolumetricCamera != null)
			{
				hxVolumetricCamera.enabled = false;
			}

			if(!menu)
			{
				this.CameraOffsetNode.transform.localPosition = Vector3.zero;
				this.CameraOffsetNode.transform.localRotation = Quaternion.identity;
			}
		}
#pragma warning restore CS0618 // Type or member is obsolete
	}
}
