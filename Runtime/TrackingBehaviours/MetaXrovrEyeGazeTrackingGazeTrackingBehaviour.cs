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

        public override float? GetViewingAngle() => 0;

        public override void StartCalibration()
            => throw new System.NotImplementedException();

        public override void StopCalibration()
            => throw new System.NotImplementedException();

        public override bool IsCalibrated => true;
        public override bool NeedsCalibration => false;
        
        public override Transform HmdTransform => _ovrCameraRig.centerEyeAnchor;
        
        private static Eye ToEye(OVREyeGaze t)
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


        protected override EyeData GenerateGazeData(GazeHit gazeHit)
            => new EyeData(HmdTransform, gazeHit, GetEye(gazeHit.GazeDetector), 
                0, 0, 0, 0);

        protected override Eye DetectEyeSide(GazeDetector gazeDetector)
            => ToEye(gazeDetector.GetComponent<OVREyeGaze>());

        protected override void AfterFilteredObjects(GazeDetector[] objects)
        {
            _ovrCameraRig = FindObjectOfType<OVRCameraRig>();
            base.AfterFilteredObjects(objects);
        }
    }
}
