using System.ComponentModel;
using OmiLAXR.TrackingBehaviours;
using UnityEngine;

namespace OmiLAXR.MetaXR.TrackingBehaviours
{
    
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / [MetaXR] <FaceState> Tracking Behaviour"), 
     Description("Tracks Face State (untested, in development).")]
    public sealed class MetaXROVRFaceStateTrackingBehaviour : ScheduledTrackingBehaviour
    {
        private OVRPlugin.FaceState _currentFaceState;
        
        protected override void Run()
        {
            var isEnabled = OVRPlugin.faceTrackingEnabled && OVRPlugin.faceTrackingSupported ||
                             OVRPlugin.faceTracking2Enabled && OVRPlugin.faceTracking2Supported;
            if (!isEnabled)
            {
                StopSchedules();
                return;
            }
            
            OVRPlugin.GetFaceState(OVRPlugin.Step.Render, -1, ref _currentFaceState);
            Debug.Log(_currentFaceState);
        }
    }
}
