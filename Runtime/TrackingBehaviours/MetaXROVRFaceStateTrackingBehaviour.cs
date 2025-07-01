using System.ComponentModel;
using OmiLAXR.Schedules;
using OmiLAXR.TrackingBehaviours;
using UnityEngine;

namespace OmiLAXR.MetaXR.TrackingBehaviours
{
    
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / MetaXR Face State Tracking Behaviour"), 
     Description("Tracks Face State (untested, in development).")]
    public class MetaXROVRFaceStateTrackingBehaviour : TrackingBehaviour
    {
        public IntervalTicker.Settings intervalSettings;
        
        private OVRPlugin.FaceState _currentFaceState;
        
        public readonly TrackingBehaviourEvent<bool> OnFaceStateTracking =
            new TrackingBehaviourEvent<bool>();
        
        protected override void OnStartedPipeline(Pipeline pipeline)
        {
            SetInterval(() =>
            {
                OnFaceStateTracking.Invoke(this, OVRPlugin.GetFaceState(OVRPlugin.Step.Render, -1, ref _currentFaceState));
            }, intervalSettings);
        }
    }
}