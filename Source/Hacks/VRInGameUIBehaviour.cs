using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

				var stageIntroCinematic = this.transform.Find("Panels/Stage/Intro/Intro Cinematic");
				if(stageIntroCinematic != null)
				{
					stageIntroCinematic.localScale = new Vector3(3, 3, 3);
				}
				else
					Plugin.loggerInstance.LogError("Could not find Stage/Intro/Intro Cinematic in hierarchy!");


				//Destroy letterbox that just won't work correctly in VR.... unless we make it absurdly big
				var Letterbox = this.transform.Find("Panels/Letterbox");
				if (Letterbox != null)
				{
					//We don't want to destroy letterbox to make sure that scripts don't break, but we will destroy images inside!
					for (int i = Letterbox.childCount - 1; i >= 0; i--)
						Destroy(Letterbox.GetChild(i).gameObject);
				}
				else
					Plugin.loggerInstance.LogError("Could not find Letterbox in hierarchy!");

				var HUD = this.transform.Find("HUD");
				if (HUD != null)
					CorrectHUD(HUD);
				else
					Plugin.loggerInstance.LogError("Could not find HUD in hierarchy!");
			}
		}

		private void CorrectHUD(Transform hud)
		{
			var ScreenFade = hud.Find("ScreenFade");
			if (ScreenFade != null)
			{
				ScreenFade.localPosition = new Vector3(0, 0, -20);
				ScreenFade.localScale = new Vector3(999, 999, 999);
			}
			else
				Plugin.loggerInstance.LogError("Could not find Screenfade in hierarchy!");

			var bottomHUD = hud.Find("BottomHud");
			if (bottomHUD != null)
			{
				var images = bottomHUD.GetComponentsInChildren<Image>(true);
				foreach(var image in images)
				{
					image.color = new Color(image.color.r, image.color.g, image.color.b, 0.85f);
				}
			}
			else
				Plugin.loggerInstance.LogError("Could not find bottomHUD in hierarchy!");

			var timeHUD = hud.Find("TimeHud");
			if (timeHUD != null)
			{
				var images = timeHUD.GetComponentsInChildren<Image>(true);
				foreach (var image in images)
				{
					image.color = new Color(image.color.r, image.color.g, image.color.b, 0.85f);
				}
			}
			else
				Plugin.loggerInstance.LogError("Could not find TimeHud in hierarchy!");
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
