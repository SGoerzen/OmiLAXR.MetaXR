using System;
using System.ComponentModel;
using Oculus.Interaction;
using OmiLAXR.TrackingBehaviours.Learner;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace OmiLAXR.MetaXR.TrackingBehaviours
{
    
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / MetaXR Pointable Unity Event Wrapper Tracking Behaviour"), 
     Description("Tracks interaction events of <PointableUnityEventWrapper> components.")]
    public class MetaXRPointableUnityEventWrapperTrackingBehaviour : InteractableTrackingBehaviour
    {
        private UnityAction<PointerEvent> _onSelectionStart;
        private UnityAction<PointerEvent> _onSelectionEnd;

        protected override void AfterFilteredObjects(Object[] objects)
        {
            var interactables = Select<PointableUnityEventWrapper>(objects);
            foreach (var interactable in interactables)
            {
                interactable.WhenSelect.AddListener(_onSelectionStart = (e) =>
                {
                    OnGrabbed.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject));
                });
                interactable.WhenUnselect.AddListener(_onSelectionEnd = (e) =>
                {
                    // TODO: Debug.Log("lastSelectExited");
                });
            }
        }

        protected override void Dispose(Object[] objects)
        {
            base.Dispose(objects);
            var interactables = Select<PointableUnityEventWrapper>(objects);
            foreach (var interactable in interactables)
            {
                interactable.WhenSelect.RemoveListener(_onSelectionStart);
                interactable.WhenUnselect.RemoveListener(_onSelectionEnd);
            }
        }
    }
}