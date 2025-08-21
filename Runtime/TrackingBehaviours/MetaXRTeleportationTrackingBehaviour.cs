using System;
using System.Collections.Generic;
using System.ComponentModel;
using Oculus.Interaction.Locomotion;
using OmiLAXR.TrackingBehaviours;
using OmiLAXR.TrackingBehaviours.Learner;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OmiLAXR.MetaXR.TrackingBehaviours {
    
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / MetaXR Teleportation Tracking Behaviour"), 
     Description("Tracks teleportation events of <TeleportInteractor> components.")]
    public class MetaXRTeleportationTrackingBehaviour : TeleportationTrackingBehaviour
    {

        struct TeleportState
        {
            public TeleportInteractor interactor;
            public Action<LocomotionEvent> action;
        }
        Dictionary<int, TeleportState> _teleportStates = new Dictionary<int, TeleportState>();
        
        protected override void AfterFilteredObjects(Object[] objects)
        {
            var teleportInteractors = Select<TeleportInteractor>(objects);
            foreach (var teleportInteractor in teleportInteractors)
            {
                Action<LocomotionEvent> action = (e) =>
                {
                    OnTeleported.Invoke(this, new TeleportationArgs()
                    {
                        CameraYOffset = Camera.main.transform.position.y,
                        TargetType = TeleportationTargetType.Floor, // TODO
                        Target = teleportInteractor.gameObject,
                        StartState = new TeleportationPlayerState()
                        {
                            FloorPosition = Camera.main.transform.position,
                            CameraGazeDirection = teleportInteractor.ArcOrigin.forward
                        },
                        EndState = new TeleportationPlayerState()
                        {
                            FloorPosition = teleportInteractor.ArcEnd.Point,
                            CameraGazeDirection = teleportInteractor.ArcOrigin.forward
                        }
                    });
                };
                teleportInteractor.WhenLocomotionPerformed += action;
                _teleportStates.Add(action.GetHashCode(), new TeleportState()
                {
                    interactor = teleportInteractor,
                    action = action
                });
            }
        }

        protected override void Dispose(Object[] objects)
        {
            foreach (var teleportStatesValue in _teleportStates.Values)
            {
                teleportStatesValue.interactor.WhenLocomotionPerformed -= teleportStatesValue.action;
            }
        }
    }
}