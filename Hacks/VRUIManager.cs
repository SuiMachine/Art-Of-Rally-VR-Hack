using System.IO;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ArtOfRallySuiVR.Hacks
{
	public class VRUIManager : MonoBehaviour
	{
		private UnityEngine.Rendering.CompareFunction desiredUIComparison = UnityEngine.Rendering.CompareFunction.Always;
		//private static Shader customUIShader;
		//private static Material customUIMaterial;

		void Start()
		{
/*			if(customUIShader == null || customUIMaterial == null)
			{
				var assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "zwriteui"));
				if(assetBundle == null)
				{
					Plugin.loggerInstance.LogError("No asset bundle loaded?!");
					return;
				}

				customUIShader = assetBundle.LoadAsset<Shader>("CustomUIShader");
				customUIMaterial = assetBundle.LoadAsset<Material>("UI_UIDefaultZwrite");
				assetBundle.Unload(false);
			}

			var canvases = this.GetComponentsInChildren<Canvas>();
			foreach (var canvas in canvases)
			{
				canvas.renderMode = RenderMode.WorldSpace;
			}*/


			var graphics = GetComponentsInChildren<UnityEngine.UI.Graphic>(true);
			foreach (var graphic in graphics)
			{
				//graphic.material = customUIMaterial;
				Material material = graphic.materialForRendering;

				if (material == null)
					continue;

				material.SetInt("unity_GUIZTestMode", (int)desiredUIComparison);
			}

			var images = GetComponentsInChildren<UnityEngine.UI.Image>(true);
			foreach (var image in images)
			{
				//image.material = customUIMaterial;
				Material material = image.materialForRendering;

				if (material == null)
					continue;

				material.SetInt("unity_GUIZTestMode", (int)desiredUIComparison);
			}
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

/*			if (this.gameObject.scene.name == "MainMenu")
			{
				this.enabled = false;
				return;
			}*/
		}
	}
}
