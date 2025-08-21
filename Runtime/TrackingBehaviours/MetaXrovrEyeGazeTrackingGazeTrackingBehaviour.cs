using System.ComponentModel;
using OmiLAXR.TrackingBehaviours.Learner.Gaze;
using OmiLAXR.Types;
using UnityEngine;

namespace OmiLAXR.MetaXR
{
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / MetaXR <OVREyeGaze> Tracking Behaviour"),
     Description("Realize the <EyeTrackingBehaviour> for MetaXR <OVREyeGaze> component.")]
    public class MetaXrovrEyeGazeTrackingGazeTrackingBehaviour : EyeGazeTrackingBehaviour
    {
        private OVRCameraRig _ovrCameraRig;
        
        public override PupilDilationData? GetPupilDilationData()
            => new PupilDilationData();

        public override double? GetViewingAngle()
            => 0;

        public override Transform HmdTransform => _ovrCameraRig.centerEyeAnchor;
        
        public override Eye GetEyeSide(OVREyeGaze t)
        {
            switch (t.Eye)
            {
                case OVREyeGaze.EyeId.Left:
                    return Eye.Left;
                case OVREyeGaze.EyeId.Right:
                    return Eye.Right;
                default:
                    return Eye.Unknown;
            }
        }
        

        protected override void AfterFilteredObjects(GazeDetector[] objects)
        {
            _ovrCameraRig = FindObjectOfType<OVRCameraRig>();
            
            base.AfterFilteredObjects(objects);
        }
    }
}
