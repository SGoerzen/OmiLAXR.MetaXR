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
    public class MetaXRInteractableUnityEventWrapperTrackingBehaviour : InteractableTrackingBehaviour
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
                    // TODO: OnReleased.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject));
                });
                interactable.WhenSelect.AddListener(_onSelectionStart = () =>
                {
                    OnInteracted.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject));
                });
                interactable.WhenUnselect.AddListener(_onSelectionEnd = () =>
                {
                    // TODO: Debug.Log("lastSelectExited");
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