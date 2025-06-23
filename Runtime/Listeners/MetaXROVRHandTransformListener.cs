using System.ComponentModel;
using OmiLAXR.Listeners;
using UnityEngine;

namespace OmiLAXR.MetaXR.Listeners
{
    [AddComponentMenu("OmiLAXR / 1) Listeners / MetaXR <OVRHand> Objects Listener"),
     Description("Provides all <OVRHand> components (left or right hand) to pipeline.")]
    public class MetaXROVRHandTransformListener : Listener
    {
        public override void StartListening()
        {
            var hands = FindObjects<OVRHand>();
            foreach (var hand in hands)
            {
                if (hand.GetHand() == OVRPlugin.Hand.HandLeft || hand.GetHand() == OVRPlugin.Hand.HandRight)
                {
                    // TODO: Check for name "LeftHandAnchor" / "RightHandAnchor"?
                    var tw = hand.gameObject.transform.parent.gameObject.GetComponent<TransformWatcher>() ?? hand.transform.parent.gameObject.AddComponent<TransformWatcher>();
                    Found(tw);
                }
            }
        }
    }
}