using System.ComponentModel;
using OmiLAXR.Listeners;
using OmiLAXR.Types;
using UnityEngine;

namespace OmiLAXR.MetaXR.Listeners
{   
    [AddComponentMenu("OmiLAXR / 1) Listeners / MetaXR <OVREyeGaze> Objects Listener"),
      Description("Provides all <OVREyeGaze> components to pipeline.")]
    public class MetaXROVREyeGazeListener : EyeInteractorListener<OVREyeGaze>
    {
        // public override bool IsEyeTrackingEnabled(OVREyeGaze t)
        // {
        //     var eyeState = new OVRPlugin.EyeGazesState();
        //     
        //     if (OVRPlugin.GetEyeGazesState(OVRPlugin.Step.Render, -1, ref eyeState))
        //     {
        //         return eyeState.EyeGazes[(int)t.Eye].IsValid;
        //     }
        //
        //     return false;
        // }
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
    }
}