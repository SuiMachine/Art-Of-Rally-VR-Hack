﻿using UnityEngine;
using UnityEngine.XR;

namespace ArtOfRallySuiVR
{
	public class VRCameraAdditionalController : MonoBehaviour
	{
		public static VRCameraAdditionalController Instance;
		
		public static void Init()
		{
			if(Instance == null)
			{
				var go = new GameObject().AddComponent<VRCameraAdditionalController>();
				DontDestroyOnLoad(go.gameObject);
				Instance = go;
			}
		}

		void Awake()
		{
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.F8))
			{
				UnityEngine.XR.XRDevice.SetTrackingSpaceType(TrackingSpaceType.Stationary);
			}
		}
	}
}
