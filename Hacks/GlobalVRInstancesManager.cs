using UnityEngine;

namespace ArtOfRallySuiVR.Hacks
{
	public class GlobalVRInstancesManager : MonoBehaviour
	{
		public static GlobalVRInstancesManager Instance { get; private set; }

		public static void Init()
		{
			if (Instance == null)
			{
				var go = new GameObject("GlobalVRInstancesManager");
				DontDestroyOnLoad(go);
				Instance = go.AddComponent<GlobalVRInstancesManager>();
			}
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.F8))
			{
				for (int i = VR_Recenter.VRCameraInstances.Count - 1; i >= 0; i--)
				{
					if (VR_Recenter.VRCameraInstances[i] == null)
						VR_Recenter.VRCameraInstances.RemoveAt(i);
					else
					{
						var cam = VR_Recenter.VRCameraInstances[i];
						cam.SetForward();
					}
				}
			}
		}

		void OnDestroy()
		{
			Instance = null;
		}
	}
}
