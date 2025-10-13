/*
* SPDX-License-Identifier: AGPL-3.0-or-later
* Copyright (C) 2025 Sergej Görzen <sergej.goerzen@gmail.com>
* This file is part of OmiLAXR.MetaXR.
*/
using System.ComponentModel;
using OmiLAXR.Listeners;
using UnityEngine;

namespace OmiLAXR.MetaXR.Listeners
{
    [AddComponentMenu("OmiLAXR / 1) Listeners / [MetaXR] <OVREyeGaze> Listener")]
    [Description("Prepares the <OVREyeGaze> components for gaze tracking.")]
    public sealed class MetaXROVREyeGazeListener : GazeDetectorListener<OVREyeGaze>
    {
        protected override bool IsCorrectComponent(OVREyeGaze component) => true;
    }
}