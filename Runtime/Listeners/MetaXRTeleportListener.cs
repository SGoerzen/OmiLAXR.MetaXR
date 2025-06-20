using System.ComponentModel;
using Oculus.Interaction.Locomotion;
using OmiLAXR.Listeners;
using UnityEngine;

namespace OmiLAXR.MetaXR.Listeners
{
    [AddComponentMenu("OmiLAXR / 1) Listeners / MetaXR <TeleportInteractor> Objects Listener"),
     Description("Provides all <TeleportInteractor> components to pipeline.")]
    public class MetaXRTeleportListener : Listener
    {
        public override void StartListening()
        {
            var gos = FindObjects<TeleportInteractor>();
            Found(gos);
        }
    }
}