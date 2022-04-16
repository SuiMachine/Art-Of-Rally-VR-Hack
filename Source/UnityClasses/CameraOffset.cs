//This is a part of Unity 2019.4.18!
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR;

namespace UnityEditor.XR.LegacyInputHelpers
{
	public enum UserRequestedTrackingMode
    {
        Default,
        Device,
        Floor,
    }

    public class CameraOffset : MonoBehaviour
    {
        const float k_DefaultCameraYOffset = 1.36144f;
        GameObject m_CameraFloorOffsetObject = null;
        /// <summary>Gets or sets the GameObject to move to desired height off the floor (defaults to this object if none provided).</summary>
        public GameObject cameraFloorOffsetObject { get { return m_CameraFloorOffsetObject; } set { m_CameraFloorOffsetObject = value; SetupCamera(); } }

        UserRequestedTrackingMode m_RequestedTrackingMode = UserRequestedTrackingMode.Default;
        public UserRequestedTrackingMode requestedTrackingMode { get { return m_RequestedTrackingMode; } set { m_RequestedTrackingMode = value; TryInitializeCamera(); } }

        /// <summary>Gets or sets the type of tracking origin to use for this Rig. Tracking origins identify where 0,0,0 is in the world of tracking. Not all devices support all tracking spaces; if the selected tracking space is not set it will fall back to Stationary.</summary>
        TrackingOriginModeFlags m_TrackingOriginMode = TrackingOriginModeFlags.Unknown;
        public TrackingOriginModeFlags TrackingOriginMode { get { return m_TrackingOriginMode; } set { m_TrackingOriginMode = value; TryInitializeCamera(); } }

        // Disable Obsolete warnings for TrackingSpaceType, explicitly to read in old data and upgrade.
#pragma warning disable 0618
        TrackingSpaceType m_TrackingSpace = TrackingSpaceType.Stationary;

        /// <summary>Gets or sets if the experience is rooms scale or stationary.  Not all devices support all tracking spaces; if the selected tracking space is not set it will fall back to Stationary.</summary>
        [Obsolete("CameraOffset.trackingSpace is obsolete.  Please use CameraOffset.trackingOriginMode.")]
        public TrackingSpaceType trackingSpace { get { return m_TrackingSpace; } set { m_TrackingSpace = value; TryInitializeCamera(); } }
#pragma warning restore 0618

        [SerializeField]
        [Tooltip("Camera Height to be used when in Device tracking space.")]
        float m_CameraYOffset = k_DefaultCameraYOffset;
        /// <summary>Gets or sets the amount the camera is offset from the floor (by moving the camera offset object).</summary>
        public float cameraYOffset { get { return m_CameraYOffset; } set { m_CameraYOffset = value; TryInitializeCamera(); } }

        // Bookkeeping to track lazy initialization of the tracking space type.
        bool m_CameraInitialized = false;
        bool m_CameraInitializing = false;

        /// <summary>
        /// Used to cache the input subsystems without creating additional garbage.
        /// </summary>
        static List<XRInputSubsystem> s_InputSubsystems = new List<XRInputSubsystem>();
        /// Utility helper to migrate from TrackingSpace to TrackingOrigin seamlessly	
        void UpgradeTrackingSpaceToTrackingOriginMode()
        {
            // Disable Obsolete warnings for TrackingSpaceType, explicitly to allow a proper upgrade path.	
#pragma warning disable 0618
            if (m_TrackingOriginMode == TrackingOriginModeFlags.Unknown && m_TrackingSpace <= TrackingSpaceType.RoomScale)
            {
                switch (m_TrackingSpace)
                {
                    case TrackingSpaceType.RoomScale:
                        {
                            m_TrackingOriginMode = TrackingOriginModeFlags.Floor;
                            break;
                        }
                    case TrackingSpaceType.Stationary:
                        {
                            m_TrackingOriginMode = TrackingOriginModeFlags.Device;
                            break;
                        }
                    default:
                        break;
                }

                // Tag is Invalid not to be used.	
                m_TrackingSpace = (TrackingSpaceType)3;
#pragma warning restore 0618
            }
        }

        void Awake()
        {
            if (!m_CameraFloorOffsetObject)
            {
                Debug.LogWarning("No camera container specified for XR Rig, using attached GameObject");
                m_CameraFloorOffsetObject = this.gameObject;
            }
        }

        void Start()
        {
            TryInitializeCamera();
        }

        void OnValidate()
        {
            UpgradeTrackingSpaceToTrackingOriginMode();
            TryInitializeCamera();
        }

        void TryInitializeCamera()
        {
           
            m_CameraInitialized = SetupCamera();
            if (!m_CameraInitialized & !m_CameraInitializing)
                StartCoroutine(RepeatInitializeCamera());
        }

        /// <summary>
        /// Repeatedly attempt to initialize the camera.
        /// </summary>
        /// <returns></returns>
        IEnumerator RepeatInitializeCamera()
        {
            m_CameraInitializing = true;
            yield return null;
            while (!m_CameraInitialized)
            {
                m_CameraInitialized = SetupCamera();
                yield return null;
            }
            m_CameraInitializing = false;
        }

        /// <summary>
        /// Handles re-centering and off-setting the camera in space depending on which tracking space it is setup in.
        /// </summary>
        bool SetupCamera()
        {
            SubsystemManager.GetInstances<XRInputSubsystem>(s_InputSubsystems);

            bool initialized = true;
            if (s_InputSubsystems.Count != 0)
            {
                for (int i = 0; i < s_InputSubsystems.Count; i++)
                {
                    initialized &= SetupCamera(s_InputSubsystems[i]);
                }
            }
            else
            {
                // Disable Obsolete warnings for TrackingSpaceType, explicitly to allow a proper upgrade path.
#pragma warning disable 0618

                if (m_RequestedTrackingMode == UserRequestedTrackingMode.Floor)
                {
                    SetupCameraLegacy(TrackingSpaceType.RoomScale);
                }
                else 
                {
                    SetupCameraLegacy(TrackingSpaceType.Stationary);
                }

#pragma warning restore 0618
            }

            return initialized;
        }

        bool SetupCamera(XRInputSubsystem subsystem)
        {
            if (subsystem == null)
                return false;

            bool trackingSettingsSet = false;

            float desiredOffset = cameraYOffset;

            var currentMode = subsystem.GetTrackingOriginMode();
            var supportedModes = subsystem.GetSupportedTrackingOriginModes();
            TrackingOriginModeFlags requestedMode = TrackingOriginModeFlags.Unknown;

            // map between the user requested options, and the actual options.
            if (m_RequestedTrackingMode == UserRequestedTrackingMode.Default)
            {
                requestedMode = currentMode;
            }
            else if(m_RequestedTrackingMode == UserRequestedTrackingMode.Device)
            {
                requestedMode = TrackingOriginModeFlags.Device;
            }
            else if (m_RequestedTrackingMode == UserRequestedTrackingMode.Floor)
            {
                requestedMode = TrackingOriginModeFlags.Floor;
            }
            else
            {
                Debug.LogWarning("Unknown Requested Tracking Mode");
            }

            // now we've mapped em. actually go set em.
            if (requestedMode == TrackingOriginModeFlags.Floor)
            {
                // We need to check for Unknown because we may not be in a state where we can read this data yet.
                if ((supportedModes & (TrackingOriginModeFlags.Floor | TrackingOriginModeFlags.Unknown)) == 0)
                {
                    Debug.LogWarning("CameraOffset.SetupCamera: Attempting to set the tracking space to Floor, but that is not supported by the SDK.");
                    m_TrackingOriginMode = subsystem.GetTrackingOriginMode();
                    return true;
                }

                if (subsystem.TrySetTrackingOriginMode(requestedMode))
                {
                    desiredOffset = 0.0f;
                    trackingSettingsSet = true;
                }
            }
            else if (requestedMode == TrackingOriginModeFlags.Device)
            {
                // We need to check for Unknown because we may not be in a state where we can read this data yet.
                if ((supportedModes & (TrackingOriginModeFlags.Device | TrackingOriginModeFlags.Unknown)) == 0)
                {
                    Debug.LogWarning("CameraOffset.SetupCamera: Attempting to set the tracking space to Device, but that is not supported by the SDK.");
                    m_TrackingOriginMode = subsystem.GetTrackingOriginMode();
                    return true;
                }

                if (subsystem.TrySetTrackingOriginMode(requestedMode))
                {
                    trackingSettingsSet = subsystem.TryRecenter();
                }
            }

            // what did we actually set?
            m_TrackingOriginMode = subsystem.GetTrackingOriginMode();

            if (trackingSettingsSet)
            {
                // Move camera to correct height
                if (m_CameraFloorOffsetObject)
                    m_CameraFloorOffsetObject.transform.localPosition = new Vector3(m_CameraFloorOffsetObject.transform.localPosition.x, desiredOffset, m_CameraFloorOffsetObject.transform.localPosition.z);

            }
            return trackingSettingsSet;
        }


        // Disable Obsolete warnings for TrackingSpaceType, explicitly to allow for using legacy data if available.
#pragma warning disable 0618
        void SetupCameraLegacy(TrackingSpaceType trackingSpace)
        {
            float cameraYOffset = m_CameraYOffset;
            XRDevice.SetTrackingSpaceType(trackingSpace);
            if (trackingSpace == TrackingSpaceType.Stationary)
                InputTracking.Recenter();
            else if (trackingSpace == TrackingSpaceType.RoomScale)
                cameraYOffset = 0;

            m_TrackingSpace = trackingSpace;

            // Move camera to correct height
            if (m_CameraFloorOffsetObject)
                m_CameraFloorOffsetObject.transform.localPosition = new Vector3(m_CameraFloorOffsetObject.transform.localPosition.x, cameraYOffset, m_CameraFloorOffsetObject.transform.localPosition.z);
        }
#pragma warning restore 0618
    }
}