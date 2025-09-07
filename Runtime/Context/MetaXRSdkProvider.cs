using System;
using OmiLAXR.Context;

namespace OmiLAXR.MetaXR.Context
{
    public class MetaXRSdkProvider : SdkProvider
    {
        public override string GetName() => "MetaXR";

        public override Version GetVersion() => OVRPlugin.version;
    }
}