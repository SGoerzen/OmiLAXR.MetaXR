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
        private bool _started;
        private double _lastGoodFrameTime; // when we last saw valid data
        private OVRPlugin.FaceState _currentFaceState;

        public override bool IsAvailable => OVRPlugin.faceTrackingSupported || OVRPlugin.faceTracking2Supported;
        // Treat as enabled if we successfully started AND saw data in the last second.
        public override bool IsEnabled =>
            _started && (Time.realtimeSinceStartupAsDouble - _lastGoodFrameTime) < 1.0;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (OVRPlugin.faceTracking2Supported) OVRPlugin.StartFaceTracking2(new []{ OVRPlugin.FaceTrackingDataSource.Visual, OVRPlugin.FaceTrackingDataSource.Audio });
            else if (OVRPlugin.faceTrackingSupported) OVRPlugin.StartFaceTracking();
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            if (OVRPlugin.faceTracking2Enabled) OVRPlugin.StopFaceTracking2();
            if (OVRPlugin.faceTrackingEnabled)  OVRPlugin.StopFaceTracking();
        }
        
        protected override FaceData FetchFaceData()
        {
            var got = OVRPlugin.GetFaceState(OVRPlugin.Step.Render, -1, ref _currentFaceState);

            if (!got ||
                _currentFaceState.ExpressionWeights == null ||
                _currentFaceState.ExpressionWeights.Length == 0)
                return null;

            // Optional: skip frame if all weights are zero (e.g. not yet tracking)
            // if (_currentFaceState.ExpressionWeights.All(w => w == 0f)) return null;

            _lastGoodFrameTime = Time.realtimeSinceStartupAsDouble;

            return new MetaXROVRFaceData(
                _currentFaceState.ExpressionWeights,
                _currentFaceState.ExpressionWeightConfidences
            );
        }

    }
}