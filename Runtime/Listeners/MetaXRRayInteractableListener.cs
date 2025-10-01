using System.ComponentModel;
using Oculus.Interaction;
using OmiLAXR.Listeners;
using UnityEngine;

namespace OmiLAXR.MetaXR.Listeners
{
    [AddComponentMenu("OmiLAXR / 1) Listeners / [MetaXR] <RayInteractable> Objects Listener"),
     Description("Provides all <RayInteractable> components to pipeline.")]
    public sealed class MetaXRRayInteractableListener : AutoListener<RayInteractable>
    {
        
    }
}