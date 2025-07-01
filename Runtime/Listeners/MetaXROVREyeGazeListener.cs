using System.ComponentModel;
using OmiLAXR.Listeners;
using UnityEngine;

namespace OmiLAXR.MetaXR.Listeners
{   
    [AddComponentMenu("OmiLAXR / 1) Listeners / MetaXR <OVREyeGaze> Objects Listener"),
      Description("Provides all <OVREyeGaze> components to pipeline.")]
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