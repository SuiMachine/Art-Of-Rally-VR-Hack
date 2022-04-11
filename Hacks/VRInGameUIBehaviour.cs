using UnityEngine;

namespace ArtOfRallySuiVR.Hacks
{
	public class VRInGameUIBehaviour : MonoBehaviour
	{
		private Canvas[] canvases;

		void Start()
		{
			Universal.ChangeUICompareZTestMode.SetGraphicsZOrderTestMode(this.gameObject);
			canvases = GetComponentsInChildren<Canvas>(true);
		}

		void LateUpdate()
		{
			var direction = Camera.current.transform.parent.parent.forward;
			var position = Camera.current.transform.position + direction * 25f;
			var rotation = Quaternion.Euler(0, 180, 0);
			this.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);

			foreach(var canvas in canvases)
			{
				canvas.transform.position = position;
				canvas.transform.LookAt(Camera.current.transform.position, Vector3.up);
				canvas.transform.rotation *= rotation;
				canvas.renderMode = RenderMode.WorldSpace;
			}
		}
	}
}
