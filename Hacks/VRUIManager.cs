using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ArtOfRallySuiVR.Hacks
{
	public class VRUIManager : MonoBehaviour
	{
		/*Camera lastCamera;

		void Start()
		{
			var canvases = this.GetComponentsInChildren<Canvas>();
			foreach (var canvas in canvases)
			{
				canvas.renderMode = RenderMode.WorldSpace;
			}
		}

		void Update()
		{
			if ((Camera.current != null && lastCamera != Camera.current) || Input.GetKeyDown(KeyCode.F8))
			{
				lastCamera = Camera.current;
				var flatRotation = Quaternion.Euler(0, Camera.current.transform.eulerAngles.y, 0);
				var canvases = this.GetComponentsInChildren<Canvas>();
				foreach (var canvas in canvases)
				{
					canvas.renderMode = RenderMode.WorldSpace;
					canvas.transform.position = lastCamera.transform.position + flatRotation * Vector3.forward * 25;
					canvas.transform.LookAt(lastCamera.transform);
					canvas.transform.eulerAngles = new Vector3(0, canvas.transform.eulerAngles.y + 180, 0);
					canvas.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
				}

				var guiLayer = LayerMask.GetMask("UI");
				var postProcess = lastCamera.GetComponent<PostProcessLayer>();
				if(postProcess != null)
				{
					postProcess.volumeLayer &= ~guiLayer;
				}
			}
		}*/
	}
}
