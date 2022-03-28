using UnityEngine;

namespace ArtOfRallySuiVR
{
	public class VRCameraAdditionalController : MonoBehaviour
	{
		public static VRCameraAdditionalController Instance;
		
		public static void Init()
		{
			if(Instance == null)
			{
				var go = new GameObject().AddComponent<VRCameraAdditionalController>();
				DontDestroyOnLoad(go.gameObject);
				Instance = go;
			}
		}

		[System.Obsolete]
		void Awake()
		{
			UnityEngine.XR.InputTracking.disablePositionalTracking = true;
		}

		[System.Obsolete]
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.F8))
			{
				UnityEngine.XR.InputTracking.Recenter();
			}
		}
	}
}
