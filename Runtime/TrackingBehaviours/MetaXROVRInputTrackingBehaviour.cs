using System.Collections.Generic;
using System.ComponentModel;
using OmiLAXR.TrackingBehaviours.Learner;
using UnityEngine;

namespace OmiLAXR.MetaXR.TrackingBehaviours
{
    
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / [MetaXR] <OVRInput> Tracking Behaviour"), 
     Description("Tracks controller button presses using <OVRInput>.")]
    public sealed class MetaXROVRInputTrackingBehaviour : InputSystemTrackingBehaviour
    {
        private readonly List<OVRInput.Button> _trackedButtons = new List<OVRInput.Button>
        {
            OVRInput.Button.One,
            OVRInput.Button.Two,
            OVRInput.Button.PrimaryThumbstick,
            OVRInput.Button.PrimaryIndexTrigger,
            OVRInput.Button.PrimaryHandTrigger,
            OVRInput.Button.Start
        };

        private void CheckButton(OVRInput.Controller controller, OVRInput.Button button)
        {
            if (OVRInput.GetDown(button, controller))
            {
                OnPressedAnyButton.Invoke(this, new InputTrackingBehaviourArgs()
                {
                    ButtonName = button.ToString(),
                    DeviceName = controller.ToString()
                });
            }
            if (OVRInput.GetUp(button, controller))
            {
                OnReleasedAnyButton.Invoke(this, new InputTrackingBehaviourArgs()
                {
                    ButtonName = button.ToString(),
                    DeviceName = controller.ToString()
                });
            }
        }
        
        protected override void Update()
        {
            foreach (var button in _trackedButtons)
            {
                CheckButton(OVRInput.Controller.LTouch, button);
                CheckButton(OVRInput.Controller.RTouch, button);
            }
        }
    }
}