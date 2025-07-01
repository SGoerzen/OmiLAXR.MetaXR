using OmiLAXR.Schedules;
using OmiLAXR.TrackingBehaviours;

namespace OmiLAXR.MetaXR.TrackingBehaviours
{
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