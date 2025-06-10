using Oculus.Interaction.Locomotion;
using OmiLAXR.Listeners;

namespace OmiLAXR.MetaXR.Listeners
{
    public class MetaXRTeleportListener : Listener
    {
        public override void StartListening()
        {
            print("StartListening MetaXR Teleport");
            var gos = FindObjects<TeleportInteractor>();
            print("FOUND GOs: " + gos);
            Found(gos);
        }
    }
}