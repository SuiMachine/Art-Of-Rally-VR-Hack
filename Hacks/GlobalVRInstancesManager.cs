using UnityEngine;

namespace ArtOfRallySuiVR.Hacks
{
	public class GlobalVRInstancesManager : MonoBehaviour
	{
		public static GlobalVRInstancesManager Instance { get; private set; }

		public static void Init()
		{
			if(Instance == null)
			{
				var go = new GameObject("GlobalVRInstancesManager");
				DontDestroyOnLoad(go);
				Instance = go.AddComponent<GlobalVRInstancesManager>();
			}
		}

		void Update()
		{
			if(Input.GetKeyDown(KeyCode.F8))
			{
				foreach (var camera in VR_Recenter.VRCameraInstances)
				{
					camera.SetForward();
				}
			}
		}

		void OnDestroy()
		{
			Instance = null;
		}
	}
}
