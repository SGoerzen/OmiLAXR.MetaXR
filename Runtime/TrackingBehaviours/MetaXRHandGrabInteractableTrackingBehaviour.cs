using System;
using System.Collections.Generic;
using System.ComponentModel;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using OmiLAXR.TrackingBehaviours.Learner;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OmiLAXR.MetaXR.TrackingBehaviours
{
    
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / [MetaXR] HandGrabInteractable Tracking Behaviour"), 
     Description("Tracks interaction events of <HandGrabInteractable> components.")]
    public sealed class MetaXRHandGrabInteractableTrackingBehaviour : InteractableTrackingBehaviour
    {
        
        struct InteractionState
        {
            public HandGrabInteractable interactor;
            public Action<InteractableStateChangeArgs> action;
        }
        Dictionary<int, InteractionState> _interactionStates = new Dictionary<int, InteractionState>();

        protected override void AfterFilteredObjects(Object[] objects)
        {
            var interactables = Select<HandGrabInteractable>(objects);
            foreach (var interactable in interactables)
            {
                Action<InteractableStateChangeArgs> action = (e) =>
                {
                    // For Lifecycle definitions see https://developers.meta.com/horizon/documentation/unity/unity-isdk-interactor-interactable-lifecycle/
                    
                    // interactor starts hovering interactable
                    if (e.PreviousState == InteractableState.Normal && e.NewState == InteractableState.Hover)
                    {
                        OnTouched.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject));
                    }
                    
                    // interactor stops hovering interactable
                    if (e.PreviousState == InteractableState.Hover && e.NewState == InteractableState.Normal)
                    {
                        OnReleased.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject));
                    }

                    // interactor starts grabbing interactable
                    if (e.PreviousState == InteractableState.Hover && e.NewState == InteractableState.Select)
                    {
                        OnGrabbed.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject));
                    }
                    
                    // interactor stops grabbing interactable
                    if (e.PreviousState == InteractableState.Select && e.NewState == InteractableState.Hover)
                    {
                        OnInteracted.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject));
                    }
                };

                interactable.WhenStateChanged += action;
                _interactionStates.Add(action.GetHashCode(), new InteractionState()
                {
                    interactor = interactable,
                    action = action
                });
            }
        }

        protected override void Dispose(Object[] objects)
        {
            foreach (var interactionStatesValue in _interactionStates.Values)
            {
                interactionStatesValue.interactor.WhenStateChanged -= interactionStatesValue.action;
            }
        }
    }
}