using System.ComponentModel;
using Oculus.Interaction;
using OmiLAXR.Listeners;
using UnityEngine;

namespace OmiLAXR.MetaXR.Listeners
{
    [AddComponentMenu("OmiLAXR / 1) Listeners / MetaXR <GrabInteractable> Objects Listener"),
     Description("Provides all <GrabInteractable> components to pipeline.")]
    public sealed class MetaXRGrabInteractableListener : AutoListener<GrabInteractable>
    {
        
    }
}