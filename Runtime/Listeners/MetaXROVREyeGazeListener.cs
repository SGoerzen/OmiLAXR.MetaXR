using System.Linq;
using OmiLAXR.Extensions;
using OmiLAXR.Listeners;
using OmiLAXR.TrackingBehaviours.Learner.Gaze;
using OmiLAXR.TrackingBehaviours.Learner.Gaze.Fixation;

namespace OmiLAXR.MetaXR.Listeners
{
    public sealed class MetaXROVREyeGazeListener : Listener
    {
        public bool enableFixation = true;
        public override void StartListening()
        {
            print("Search for OVREyeGaze");
            var eyeGazes = FindObjects<OVREyeGaze>();
            print($"Found {eyeGazes.Length} <OVREyeGaze> objects.");
            var gds = eyeGazes.Select(eg =>
            {
                print("Found " + eg.gameObject.name);
                var go = eg.gameObject;
                var gd = go.EnsureComponent<GazeDetector>();
                if (enableFixation)
                    go.EnsureComponent<FixationDetector>();
                return gd;
            });
            Found(gds.ToArray());
        }
    }
}