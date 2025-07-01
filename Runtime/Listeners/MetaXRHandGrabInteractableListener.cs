using System.ComponentModel;
using Oculus.Interaction.HandGrab;
using OmiLAXR.Listeners;
using UnityEngine;

namespace OmiLAXR.MetaXR.Listeners
{
    [AddComponentMenu("OmiLAXR / 1) Listeners / MetaXR <HandGrabInteractable> Objects Listener"),
     Description("Provides all <HandGrabInteractable> components to pipeline.")]
    public class MetaXRHandGrabInteractableListener : AutoListener<HandGrabInteractable>
    {
        
    }
}