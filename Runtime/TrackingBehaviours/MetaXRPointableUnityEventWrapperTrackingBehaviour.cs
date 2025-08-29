using System.ComponentModel;
using Oculus.Interaction;
using OmiLAXR.TrackingBehaviours.Learner;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace OmiLAXR.MetaXR.TrackingBehaviours
{
    
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / [MetaXR] <PointableUnityEventWrapper> Tracking Behaviour"), 
     Description("Tracks interaction events of <PointableUnityEventWrapper> components.")]
    public sealed class MetaXRPointableUnityEventWrapperTrackingBehaviour : InteractableTrackingBehaviour
    {
        private UnityAction<PointerEvent> _onHoverStart;
        private UnityAction<PointerEvent> _onHoverEnd;
        private UnityAction<PointerEvent> _onSelectionStart;
        private UnityAction<PointerEvent> _onSelectionEnd;

        protected override void AfterFilteredObjects(Object[] objects)
        {
            var interactables = Select<PointableUnityEventWrapper>(objects);
            foreach (var interactable in interactables)
            {
                interactable.WhenHover.AddListener(_onHoverStart = (e) =>
                {
                    OnTouched.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject));
                });
                interactable.WhenUnhover.AddListener(_onHoverEnd = (e) =>
                {
                    OnReleased.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject));
                });
                interactable.WhenSelect.AddListener(_onSelectionStart = (e) =>
                {
                    OnGrabbed.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject));
                });
                interactable.WhenUnselect.AddListener(_onSelectionEnd = (e) =>
                {
                    OnReleased.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject));
                });
            }
        }

        protected override void Dispose(Object[] objects)
        {
            base.Dispose(objects);
            var interactables = Select<PointableUnityEventWrapper>(objects);
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