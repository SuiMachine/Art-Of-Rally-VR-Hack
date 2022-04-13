using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArtOfRallySuiVR.Hacks
{
	public class VRInGameUIBehaviour : MonoBehaviour
	{
		private Canvas mainCanvas;
		private Canvas[] canvases;
		private List<Transform> otherTransforms = new List<Transform>();

		void Start()
		{
			Universal.ChangeUICompareZTestMode.SetGraphicsZOrderTestMode(this.gameObject);
			mainCanvas = this.GetComponent<Canvas>();
			canvases = GetComponentsInChildren<Canvas>(true).Where(x => x != mainCanvas).ToArray();

			{
				var smallBg = this.transform.Find("Panels/StageBackgrounds/SmallBg");
				var medBg = this.transform.Find("Panels/StageBackgrounds/MedBg");
				var largeBG = this.transform.Find("Panels/StageBackgrounds/LargeBg");
				if (smallBg != null)
					otherTransforms.Add(smallBg);
				else
					Plugin.loggerInstance.LogError("Could not find Small BG in hierarchy!");

				if (medBg != null)
					otherTransforms.Add(medBg);
				else
					Plugin.loggerInstance.LogError("Could not find Medium BG in hierarchy!");

				if (largeBG != null)
					otherTransforms.Add(largeBG);
				else
					Plugin.loggerInstance.LogError("Could not find Large BG in hierarchy!");
			}


		}

		void LateUpdate()
		{
			var direction = Camera.current.transform.parent.parent.forward;
			var position = Camera.current.transform.position + direction * 25f;
			var rotation = Quaternion.Euler(0, 180, 0);
			this.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

			//Main canvas
			mainCanvas.transform.position = position;
			mainCanvas.transform.LookAt(Camera.current.transform.position, Vector3.up);
			mainCanvas.transform.rotation *= rotation;
			mainCanvas.renderMode = RenderMode.WorldSpace;

			//Sub canvases (I have no idea what is the point of them)
			foreach (var canvas in canvases)
			{
				canvas.transform.LookAt(Camera.current.transform.position, Vector3.up);
				canvas.transform.rotation *= rotation;
				canvas.renderMode = RenderMode.WorldSpace;
			}

			foreach(var transform in otherTransforms)
			{
				transform.transform.LookAt(Camera.current.transform.position, Vector3.up);
				transform.transform.rotation *= rotation;
			}
		}
	}
}
