using System.ComponentModel;
using OmiLAXR.TrackingBehaviours.Learner.EyeTracking;
using UnityEngine;

namespace OmiLAXR.MetaXR
{
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / MetaXR <OVREyeGaze> Tracking Behaviour"),
     Description("Realize the <EyeTrackingBehaviour> for MetaXR <OVREyeGaze> component.")]
    public class MetaXROVREyeGazeTrackingBehaviour : EyeTrackingBehaviour
    {
        public override PupilDilationData? GetPupilDilationData()
            => new PupilDilationData();

        public override double? GetViewingAngle()
            => 0;

        protected override void AfterFilteredObjects(EyeInteractor[] objects)
        {
            foreach (var ei in objects)
            {
                var c = ei.GetComponent<OVREyeGaze>();
                if (!c)
                    continue;
                if (!leftEye && c.Eye == OVREyeGaze.EyeId.Left)
                {
                    leftEye = ei;
                }

                if (!rightEye && c.Eye == OVREyeGaze.EyeId.Right)
                {
                    rightEye = ei;
                }
            }
        }
    }
}
