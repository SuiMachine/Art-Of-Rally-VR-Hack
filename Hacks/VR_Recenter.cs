using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArtOfRallySuiVR.Hacks
{
	public class VR_Recenter : MonoBehaviour
	{
		public static List<VR_Recenter> VRCameraInstances = new List<VR_Recenter>();
		Transform mainVRTransform;
		Transform reorientNodeTransform;
		Transform recenterTransform;

		void Awake()
		{
			if(!this.transform.parent.name.StartsWith("VR"))
			{
				var mainVRNode = new GameObject("VRMainNode");
				mainVRNode.transform.position = this.transform.position;
				mainVRNode.transform.rotation = this.transform.rotation;
				mainVRNode.transform.localScale = this.transform.localScale;
				mainVRNode.transform.SetParent(this.transform.parent, true);
				mainVRTransform = mainVRNode.transform;

				var reorientNode = new GameObject("VRReorientNode");
				reorientNode.transform.SetParent(mainVRTransform, true);
				reorientNode.transform.localRotation = Quaternion.identity;
				reorientNode.transform.localPosition = Vector3.zero;
				reorientNodeTransform = reorientNode.transform;

				var recenterNode = new GameObject("VRRecenterNode");
				this.recenterTransform = recenterNode.transform;
				recenterNode.transform.SetParent(reorientNode.transform, true);
				this.recenterTransform.localPosition = Vector3.zero;
				this.recenterTransform.localRotation = Quaternion.identity;
				this.transform.SetParent(recenterNode.transform, true);
				VRCameraInstances.Add(this);
			}
		}

		internal void SetForward()
		{
			var rotationCamera = mainVRTransform.InverseTransformDirection(this.transform.forward);
			reorientNodeTransform.localEulerAngles = -rotationCamera;
			reorientNodeTransform.eulerAngles = new Vector3(0, reorientNodeTransform.eulerAngles.y - 90, 0);
			
		}

		void FixedUpdate()
		{
			recenterTransform.localPosition = -this.transform.localPosition;
			recenterTransform.localRotation = Quaternion.identity;
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.F8))
				SetForward();
		}

		void OnDestroy()
		{
			if (VRCameraInstances.Contains(this))
				VRCameraInstances.Remove(this);
		}
	}
}
