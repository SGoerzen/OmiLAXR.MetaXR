using System;
using OmiLAXR.Context;
using UnityEngine;

namespace OmiLAXR.MetaXR.Context
{
    [AddComponentMenu("OmiLAXR / Scenario Context / [MetaXR] SDK Provider")]
    public class MetaXRSdkProvider : SdkProvider
    {
        public override string GetName() => "MetaXR";

        public override Version GetVersion() => OVRPlugin.version;
    }
}