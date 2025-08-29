using System.ComponentModel;
using OmiLAXR.TrackingBehaviours;
using UnityEngine;

namespace OmiLAXR.MetaXR.TrackingBehaviours
{
    
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / MetaXR Face State Tracking Behaviour"), 
     Description("Tracks Face State (untested, in development).")]
    public sealed class MetaXROVRFaceStateTrackingBehaviour : IntervalTrackingBehaviour
    {
        private OVRPlugin.FaceState _currentFaceState;
        
        public readonly TrackingBehaviourEvent<float[]> OnFaceStateTracking =
            new TrackingBehaviourEvent<float[]>();

        private void Awake()
        {
            isIntervalActivated = OVRPlugin.faceTrackingEnabled && OVRPlugin.faceTrackingSupported ||
                                  OVRPlugin.faceTracking2Enabled && OVRPlugin.faceTracking2Supported;
        }

        protected override void IntervalUpdate()
        {
            OVRPlugin.GetFaceState(OVRPlugin.Step.Render, -1, ref _currentFaceState);
            Debug.Log(_currentFaceState);
            OnFaceStateTracking.Invoke(this, _currentFaceState.ExpressionWeights);
        }
    }
}
