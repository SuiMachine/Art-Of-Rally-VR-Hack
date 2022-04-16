using System;
using System.Collections;
using UnityEngine;

namespace ArtOfRallySuiVR.Hacks
{
	public class VRMainMenuBehaviour : MonoBehaviour
	{
		private const float SCALE = 0.005f;
		private static VRMainMenuBehaviour instance;
		private static VR_Recenter VrRecenterInstance;

		Canvas mainCanvas;

		void Awake()
		{
			mainCanvas = this.GetComponent<Canvas>();
			Universal.ChangeUICompareZTestMode.SetGraphicsZOrderTestMode(mainCanvas.gameObject);
			mainCanvas.renderMode = RenderMode.WorldSpace;
			instance = this;
		}

		void Update()
		{
			if(VrRecenterInstance != null)
			{
				mainCanvas.transform.LookAt(VrRecenterInstance.transform);
				mainCanvas.transform.localRotation *= Quaternion.Euler(0, 180, 0);
				mainCanvas.transform.localScale = Vector3.one * SCALE;
			}
		}

		void StartRepostion() => StartCoroutine(Reposition());
		internal static void RegisterCamera(VR_Recenter VrRecenterInstanceP) => VrRecenterInstance = VrRecenterInstanceP;

		private IEnumerator Reposition()
		{
			//This ia stupid hack :(
			yield return new WaitForSeconds(1);
			if (VrRecenterInstance != null)
			{
				var flatRotation = Quaternion.Euler(0, VrRecenterInstance.transform.eulerAngles.y, 0);
				mainCanvas.transform.position = VrRecenterInstance.transform.position + flatRotation * Vector3.forward * 5;
			}
			else
				Plugin.loggerInstance.LogError("No camera cought in time :(");
		}

		internal static void RepositionMenu()
		{
			if(instance != null)
			{
				instance.StartRepostion();
			}
		}
	}
}
