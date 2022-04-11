using UnityEngine;

namespace ArtOfRallySuiVR.Hacks.Universal
{
	public static class ChangeUICompareZTestMode
	{
		private const UnityEngine.Rendering.CompareFunction desiredUIComparison = UnityEngine.Rendering.CompareFunction.Always;

		public static void SetGraphicsZOrderTestMode(GameObject go)
		{
			var graphics = go.GetComponentsInChildren<UnityEngine.UI.Graphic>(true);
			foreach (var graphic in graphics)
			{
				//graphic.material = customUIMaterial;
				Material material = graphic.materialForRendering;

				if (material == null)
					continue;

				material.SetInt("unity_GUIZTestMode", (int)desiredUIComparison);
			}
		}

		public static void SetImagesZOrderTestMOde(GameObject go)
		{
			var images = go.GetComponentsInChildren<UnityEngine.UI.Image>(true);
			foreach (var image in images)
			{
				//image.material = customUIMaterial;
				Material material = image.materialForRendering;

				if (material == null)
					continue;

				material.SetInt("unity_GUIZTestMode", (int)desiredUIComparison);
			}
		}
	}
}
