using System.ComponentModel;
using OmiLAXR.Components;
using OmiLAXR.MetaXR.Components;
using OmiLAXR.TrackingBehaviours.Learner;
using UnityEngine;

namespace OmiLAXR.MetaXR.TrackingBehaviours
{
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / [MetaXR] <FaceState> Tracking Behaviour (Alpha)"),
     Description("Tracks Face State (untested, in development).")]
    public sealed class MetaXROVRFaceStateTrackingBehaviour : FacialTrackingBehaviour
    {
        private OVRPlugin.FaceState _currentFaceState;

        public override bool IsAvailable => OVRPlugin.faceTrackingSupported || OVRPlugin.faceTracking2Supported;
        public override bool IsEnabled => OVRPlugin.faceTrackingEnabled || OVRPlugin.faceTracking2Enabled;

        protected override FaceData FetchFaceData()
        {
            // Query latest face state from OVR
            OVRPlugin.GetFaceState(OVRPlugin.Step.Render, -1, ref _currentFaceState);

            // Wrap as FaceData for your existing pipeline
            return new MetaXROVRFaceData(
                _currentFaceState.ExpressionWeights,
                _currentFaceState.ExpressionWeightConfidences
            );
        }
    }
}