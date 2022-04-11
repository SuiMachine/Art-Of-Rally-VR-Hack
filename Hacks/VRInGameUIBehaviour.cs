using UnityEngine;

namespace ArtOfRallySuiVR.Hacks
{
	public class VRInGameUIBehaviour : MonoBehaviour
	{
		void Start()
		{
			Universal.ChangeUICompareZTestMode.SetGraphicsZOrderTestMode(this.gameObject);
		}

		Canvas[] canvases = null;

		void Update()
		{
			if(canvases == null)
				canvases = this.GetComponentsInChildren<Canvas>(true);

			var flatRotation = Quaternion.Euler(0, Camera.current.transform.eulerAngles.y, 0);
			foreach (var canvas in canvases)
			{
				if (canvas != null)
				{
					canvas.renderMode = RenderMode.WorldSpace;
					canvas.transform.position = Camera.current.transform.position + flatRotation * Vector3.forward * 25;
					canvas.transform.LookAt(Camera.current.transform);
					canvas.transform.eulerAngles = new Vector3(0, canvas.transform.eulerAngles.y + 180, 0);
					canvas.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
				}
			}

			if (this.gameObject.scene.name == "MainMenu")
			{
				this.enabled = false;
				return;
			}
		}
	}
}
