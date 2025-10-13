/*
* SPDX-License-Identifier: AGPL-3.0-or-later
* Copyright (C) 2025 Sergej Görzen <sergej.goerzen@gmail.com>
* This file is part of OmiLAXR.MetaXR.
*/
using System.ComponentModel;
using Oculus.Interaction;
using OmiLAXR.Listeners;
using UnityEngine;

namespace OmiLAXR.MetaXR.Listeners
{
    [AddComponentMenu("OmiLAXR / 1) Listeners / [MetaXR] <PointableUnityEventWrapper> Objects Listener"),
     Description("Provides all <PointableUnityEventWrapper> components to pipeline.")]
    public sealed class MetaXRPointableUnityEventWrapperListener : AutoListener<PointableUnityEventWrapper>
    {
        
    }
}