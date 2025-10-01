using System;
using System.ComponentModel;
using OmiLAXR.Components.Gaze;
using OmiLAXR.TrackingBehaviours.Learner.Gaze;
using OmiLAXR.Types;
using UnityEngine;

namespace OmiLAXR.MetaXR
{
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / [MetaXR] <OVREyeGaze> Tracking Behaviour (Beta)"),
     Description("Realize the <EyeTrackingBehaviour> for MetaXR <OVREyeGaze> component.")]
    public sealed class MetaXROVREyeGazeTrackingBehaviour : EyeGazeTrackingBehaviour
    {
        public override string DeviceName => OVRPlugin.productName;
        private OVRCameraRig _ovrCameraRig;
        private OVRPlugin.EyeGazesState _eyeGazesState;
        private OVRFaceExpressions _ovrFaceExpressions;

        public override Transform HmdTransform => _ovrCameraRig.centerEyeAnchor;

        private void Awake()
        {
            _ovrFaceExpressions = FindObjectOfType<OVRFaceExpressions>();
            if (!_ovrFaceExpressions)
            {
                DebugLog.OmiLAXR.Warning("No OVRFaceExpressions found in scene. A <OVRFaceExpressions> component will be added automatically.");
                _ovrFaceExpressions = gameObject.AddComponent<OVRFaceExpressions>();
            }
        }

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

        private static OVRPlugin.Eye ToOvrEye(Eye eye)
        {
            switch (eye)
            {
                case Eye.Left:
                    return OVRPlugin.Eye.Left;
                case Eye.Right:
                    return OVRPlugin.Eye.Right;
                default:
                    return OVRPlugin.Eye.None;
            }
        }

        // Frustumf: already has fovX / fovY (in degrees)
        private static Frustum ToFrustum(OVRPlugin.Frustumf frustum)
            => new Frustum(frustum.zNear, frustum.zFar, frustum.fovX, frustum.fovY);

        // Frustumf2: has Fovf with tangents; convert to full FOV angles
        private static Frustum ToFrustum(OVRPlugin.Frustumf2 frustum)
        {
            var f = frustum.Fov; // OVRPlugin.Fovf: LeftTan, RightTan, UpTan, DownTan

            // Full horizontal/vertical FOV in degrees:
            var fovX = Mathf.Rad2Deg * (Mathf.Atan(f.LeftTan) + Mathf.Atan(f.RightTan));
            var fovY = Mathf.Rad2Deg * (Mathf.Atan(f.UpTan)   + Mathf.Atan(f.DownTan));

            return new Frustum(frustum.zNear, frustum.zFar, fovX, fovY);
        }
        
        /// <summary>
        /// Reads eyelid openness for a single eye from OVRFaceExpressions.
        /// Returns true if a valid value was read this frame.
        /// </summary>
        public bool TryGetOpenness(OVRPlugin.Eye eye, out float openness)
        {
            openness = -1f;

            if (_ovrFaceExpressions == null || !_ovrFaceExpressions.ValidExpressions)
                return false;

            // Map eye -> corresponding "EyesClosed" blendshape
            var expr =
                (eye == OVRPlugin.Eye.Left)
                    ? OVRFaceExpressions.FaceExpression.EyesClosedL
                    : OVRFaceExpressions.FaceExpression.EyesClosedR;

            if (!_ovrFaceExpressions.TryGetFaceExpressionWeight(expr, out var closed))
                return false;

            openness = 1f - Mathf.Clamp01(closed);  // 1 = fully open, 0 = fully closed
            return true;
        }
        
        protected override EyeData GenerateGazeData(GazeHit gazeHit)
        {
            var owner = gazeHit.GazeDetector.GetOwner<OVREyeGaze>();
            var eye = GetEye(gazeHit.GazeDetector);
            var ovrEye = ToOvrEye(eye);
            var frustum = ToFrustum(OVRPlugin.GetEyeFrustum(ovrEye));
            TryGetOpenness(ovrEye, out var openness);
            return new EyeData(gazeHit, 
                gazeOriginWorld: HmdTransform.position, 
                gazePointWorld: gazeHit.Target.transform.position, 
                eye, 
                frustum,
                openness, 
                owner.Confidence, 
                OVRPlugin.eyeDepth, 
                OVRPlugin.eyeHeight); // Pupil diameter / dilation is not supported
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
