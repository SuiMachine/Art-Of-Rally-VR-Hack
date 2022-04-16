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

		void OnDestroy()
		{
			Instance = null;
		}
	}
}
