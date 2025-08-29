using System.ComponentModel;
using OmiLAXR.TrackingBehaviours.Learner.Gaze;
using OmiLAXR.Types;
using UnityEngine;

namespace OmiLAXR.MetaXR
{
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / [MetaXR] <OVREyeGaze> Tracking Behaviour"),
     Description("Realize the <EyeTrackingBehaviour> for MetaXR <OVREyeGaze> component.")]
    public sealed class MetaXROVREyeGazeTrackingBehaviour : EyeGazeTrackingBehaviour
    {
        private OVRCameraRig _ovrCameraRig;
        
        public override Transform HmdTransform => _ovrCameraRig.centerEyeAnchor;
        
        private static Eye ToEye(OVREyeGaze t)
        {
            if (t == null)
                return Eye.Unknown;
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
        
        protected override EyeData GenerateGazeData(GazeHit gazeHit)
        {
            var owner = gazeHit.GazeDetector.GetOwner<OVREyeGaze>();
            return new EyeData(gazeOriginWorld: HmdTransform.position, 
                gazePointWorld: gazeHit.Target.transform.position, 
                GetEye(gazeHit.GazeDetector), 
                -1, owner.Confidence, -1, -1); // Pupil diameter / dilation is not supported
        }

        protected override Eye DetectEyeSide(GazeDetector gazeDetector)
            => ToEye(gazeDetector.GetOwner<OVREyeGaze>());

        protected override void AfterFilteredObjects(GazeDetector[] objects)
        {
            _ovrCameraRig = FindObjectOfType<OVRCameraRig>();
            AutoAssignOwners<OVREyeGaze>(objects);
            base.AfterFilteredObjects(objects);
        }
    }
}
