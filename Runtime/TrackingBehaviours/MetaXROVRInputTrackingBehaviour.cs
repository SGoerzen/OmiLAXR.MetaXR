using System.Collections.Generic;
using System.ComponentModel;
using OmiLAXR.TrackingBehaviours.Learner;
using UnityEngine;

namespace OmiLAXR.MetaXR.TrackingBehaviours
{
    
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / MetaXR OVRInput Tracking Behaviour"), 
     Description("Tracks controller button presses using <OVRInput>.")]
    public class MetaXROVRInputTrackingBehaviour : InputSystemTrackingBehaviour
    {

        private List<OVRInput.Controller> _trackedControllers = new List<OVRInput.Controller>
        {
            OVRInput.Controller.LTouch,
            OVRInput.Controller.RTouch
        };
        
        private List<OVRInput.Button> _trackedButtons = new List<OVRInput.Button>
        {
            OVRInput.Button.One,
            OVRInput.Button.Two,
            OVRInput.Button.PrimaryThumbstick,
            OVRInput.Button.PrimaryIndexTrigger,
            OVRInput.Button.PrimaryHandTrigger,
            OVRInput.Button.Start
        };
        
        protected override void Update()
        {
            foreach (var controller in _trackedControllers)
            {
                foreach (var button in _trackedButtons)
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
            }
        }
    }
}