using System.ComponentModel;
using Oculus.Interaction;
using OmiLAXR.Listeners;
using UnityEngine;

namespace OmiLAXR.MetaXR.Listeners
{
    [AddComponentMenu("OmiLAXR / 1) Listeners / MetaXR <InteractableUnityEventWrapper> Objects Listener"),
     Description("Provides all <InteractableUnityEventWrapper> components to pipeline.")]
    public sealed class MetaXRInteractableUnityEventWrapperListener : AutoListener<InteractableUnityEventWrapper>
    {
        
    }
}