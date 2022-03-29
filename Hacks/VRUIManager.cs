using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ArtOfRallySuiVR.Hacks
{
	public class VRUIManager : MonoBehaviour
	{
		private UnityEngine.Rendering.CompareFunction desiredUIComparison = UnityEngine.Rendering.CompareFunction.Always;

		void Start()
		{
			var canvases = this.GetComponentsInChildren<Canvas>();
			foreach (var canvas in canvases)
			{
				canvas.renderMode = RenderMode.WorldSpace;
			}


			var graphics = GetComponentsInChildren<UnityEngine.UI.Graphic>();
			foreach (var graphic in graphics)
			{
				Material material = graphic.materialForRendering;

				if (material == null)
					continue;

				material.SetInt("unity_GUIZTestMode", (int)desiredUIComparison);
			}

			var guiLayer = LayerMask.GetMask("UI");
			var cameras = Resources.FindObjectsOfTypeAll<Camera>();
			foreach(var camera in cameras)
			{
				var postProcess = camera.GetComponent<PostProcessLayer>();
				if (postProcess != null)
				{
					postProcess.volumeLayer &= ~guiLayer;
				}
			}
		}

		void Update()
		{
			var flatRotation = Quaternion.Euler(0, Camera.current.transform.eulerAngles.y, 0);
			var canvases = this.GetComponentsInChildren<Canvas>();
			foreach (var canvas in canvases)
			{
				canvas.renderMode = RenderMode.WorldSpace;
				canvas.transform.position = Camera.current.transform.position + flatRotation * Vector3.forward * 25;
				canvas.transform.LookAt(Camera.current.transform);
				canvas.transform.eulerAngles = new Vector3(0, canvas.transform.eulerAngles.y + 180, 0);
				canvas.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
			}

			if (this.gameObject.scene.name == "MainMenu")
				this.enabled = false;
		}
	}
}
