using System;
using System.ComponentModel;
using OmiLAXR.Components;
using OmiLAXR.Listeners;
using UnityEngine;

namespace OmiLAXR.MetaXR.Listeners
{
    [AddComponentMenu("OmiLAXR / 1) Listeners / [MetaXR] <OVRHand> Objects Listener"),
     Description("Provides all <OVRHand> components (left or right hand) to pipeline.")]
    public sealed class MetaXROVRHandTransformListener : Listener
    {
        public bool ignoreLeftHand;
        public bool ignoreRightHand;
        
        public override void StartListening()
        {
            var hands = FindObjects<OVRHand>();
            foreach (var hand in hands)
            {
                if (hand.GetHand() == OVRPlugin.Hand.HandLeft || hand.GetHand() == OVRPlugin.Hand.HandRight)
                {
                    // TODO: Check for name "LeftHandAnchor" / "RightHandAnchor"?
                    if (hand.GetHand() == OVRPlugin.Hand.HandLeft && !ignoreLeftHand)
                    {
                        var tw = hand.gameObject.transform.parent.gameObject.GetComponent<TransformWatcher>() ?? hand.transform.parent.gameObject.AddComponent<TransformWatcher>();
                        Found(tw);
                    }
                    
                    if (hand.GetHand() == OVRPlugin.Hand.HandRight && !ignoreRightHand)
                    {
                        var tw = hand.gameObject.transform.parent.gameObject.GetComponent<TransformWatcher>() ?? hand.transform.parent.gameObject.AddComponent<TransformWatcher>();
                        Found(tw);
                    }
                }
            }
        }
    }
}