using System;
using System.ComponentModel;
using Oculus.Interaction;
using OmiLAXR.TrackingBehaviours.Learner;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace OmiLAXR.MetaXR.TrackingBehaviours
{
    
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / MetaXR Interactable Unity Event Wrapper Tracking Behaviour"), 
     Description("Tracks interaction events of <InteractableUnityEventWrapper> components.")]
    public sealed class MetaXRInteractableUnityEventWrapperTrackingBehaviour : InteractableTrackingBehaviour
    {
        private UnityAction _onHoverStart;
        private UnityAction _onHoverEnd;
        private UnityAction _onSelectionStart;
        private UnityAction _onSelectionEnd;

        protected override void AfterFilteredObjects(Object[] objects)
        {
            var interactables = Select<InteractableUnityEventWrapper>(objects);
            foreach (var interactable in interactables)
            {
                interactable.WhenHover.AddListener( _onHoverStart = () =>
                {
                    OnTouched.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject));
                });
                interactable.WhenUnhover.AddListener(_onHoverEnd = () =>
                {
                    OnReleased.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject));
                });
                interactable.WhenSelect.AddListener(_onSelectionStart = () =>
                {
                    OnGrabbed.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject));
                });
                interactable.WhenUnselect.AddListener(_onSelectionEnd = () =>
                {
                    OnInteracted.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject));
                });
            }
        }

        protected override void Dispose(Object[] objects)
        {
            base.Dispose(objects);
            var interactables = Select<InteractableUnityEventWrapper>(objects);
            foreach (var interactable in interactables)
            {
                interactable.WhenHover.RemoveListener(_onHoverStart);
                interactable.WhenUnhover.RemoveListener(_onHoverEnd);
                interactable.WhenSelect.RemoveListener(_onSelectionStart);
                interactable.WhenUnselect.RemoveListener(_onSelectionEnd);
            }
        }
    }
}