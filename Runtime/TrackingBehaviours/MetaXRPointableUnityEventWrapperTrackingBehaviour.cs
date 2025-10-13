/*
* SPDX-License-Identifier: AGPL-3.0-or-later
* Copyright (C) 2025 Sergej Görzen <sergej.goerzen@gmail.com>
* This file is part of OmiLAXR.MetaXR.
*/
using System.ComponentModel;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using OmiLAXR.TrackingBehaviours.Learner;
using UnityEngine;
using UnityEngine.Events;
using Component = UnityEngine.Component;
using Hand = OmiLAXR.Types.Hand;
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
                    OnPointed.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject, GetInteractingHand(e)));
                });
                interactable.WhenUnhover.AddListener(_onHoverEnd = (e) =>
                {
                    OnReleased.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject, GetInteractingHand(e)));
                });
                interactable.WhenSelect.AddListener(_onSelectionStart = (e) =>
                {
                    OnGrabbed.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject, GetInteractingHand(e)));
                });
                interactable.WhenUnselect.AddListener(_onSelectionEnd = (e) =>
                {
                    OnReleased.Invoke(this, new InteractableEventArgs(interactable.transform.gameObject, GetInteractingHand(e)));
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

        private Hand GetInteractingHand(PointerEvent e)
        {
            if (e.Data == null)
                return Hand.Unknown;

            var pointer = e.Data;
            var pointerObj = pointer as Component; // Zugriff auf GameObject

            if (!pointerObj)
                return Hand.Unknown;

            // Hand-Tracking?
            var handRef = pointerObj.GetComponentInParent<HandRef>();
            if (handRef)
            {
                if (handRef.Handedness == Handedness.Right)
                {
                    return Hand.Right;
                }

                if (handRef.Handedness == Handedness.Left)
                {
                    return Hand.Left;
                }
            }

            // Controller-Tracking?
            var controllerRef = pointerObj.GetComponentInParent<ControllerRef>();
            if (controllerRef)
            {
                if (controllerRef.Handedness == Handedness.Right)
                {
                    return Hand.Right;
                }

                if (controllerRef.Handedness == Handedness.Left)
                {
                    return Hand.Left;
                }
            }
            return Hand.Unknown;
        }
    }
}