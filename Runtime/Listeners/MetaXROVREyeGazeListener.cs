using OmiLAXR.Listeners;

namespace OmiLAXR.MetaXR.Listeners
{
    public class MetaXROVREyeGazeListener : Listener
    {
        public override void StartListening()
        {
            var eyeGazes = FindObjects<OVREyeGaze>();
            foreach (var eyeGaze in eyeGazes)
            {
                if (eyeGaze.EyeTrackingEnabled)
                {
                    var tw = eyeGaze.gameObject.transform.gameObject.GetComponent<TransformWatcher>() ?? eyeGaze.transform.gameObject.AddComponent<TransformWatcher>();
                    Found(tw);
                }
            }
        }
    }
}