/*
* SPDX-License-Identifier: AGPL-3.0-or-later
* Copyright (C) 2025 Sergej Görzen <sergej.goerzen@gmail.com>
* This file is part of OmiLAXR.MetaXR.
*/
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