using System.ComponentModel;
using OmiLAXR.TrackingBehaviours.Learner.EyeTracking;
using UnityEngine;

namespace OmiLAXR.MetaXR
{
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / MetaXR <OVREyeGaze> Tracking Behaviour"),
     Description("Realize the <EyeTrackingBehaviour> for MetaXR <OVREyeGaze> component.")]
    public class MetaXROVREyeGazeTrackingBehaviour : EyeTrackingBehaviour
    {
        private OVRCameraRig _ovrCameraRig;
        
        public override PupilDilationData? GetPupilDilationData()
            => new PupilDilationData();

        public override double? GetViewingAngle()
            => 0;

        public override Transform HmdTransform => _ovrCameraRig.centerEyeAnchor;

        protected override void AfterFilteredObjects(EyeInteractor[] objects)
        {
            _ovrCameraRig = FindObjectOfType<OVRCameraRig>();
            
            base.AfterFilteredObjects(objects);
        }
    }
}
