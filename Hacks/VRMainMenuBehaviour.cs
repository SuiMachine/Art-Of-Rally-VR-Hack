using UnityEngine;

namespace ArtOfRallySuiVR.Hacks
{
	public class VRMainMenuBehaviour : MonoBehaviour
	{
		Canvas mainCanvas;
		private Vector3 lastCameraPosition;

		void Awake()
		{
			mainCanvas = this.GetComponent<Canvas>();
			Universal.ChangeUICompareZTestMode.SetGraphicsZOrderTestMode(mainCanvas.gameObject);
			mainCanvas.renderMode = RenderMode.WorldSpace;
		}

		void Update()
		{
			if (UnityEngine.XR.XRSettings.enabled && UnityEngine.XR.XRSettings.isDeviceActive)
			{
				if (lastCameraPosition != Camera.current.transform.position)
				{
					var scale = 0.01f;
					var flatRotation = Quaternion.Euler(0, Camera.current.transform.eulerAngles.y, 0);
					mainCanvas.transform.position = Camera.current.transform.position + flatRotation * Vector3.forward * 10;
					mainCanvas.transform.position += Vector3.up * (1080 * scale / 2);
					mainCanvas.transform.LookAt(Camera.current.transform);
					mainCanvas.transform.localScale = Vector3.one * scale;
					mainCanvas.transform.localRotation *= Quaternion.Euler(0, 180, -17);
					lastCameraPosition = Camera.current.transform.position;
				}
			}
		}
	}
}
