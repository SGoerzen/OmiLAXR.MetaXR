/*
* SPDX-License-Identifier: AGPL-3.0-or-later
* Copyright (C) 2025 Sergej Görzen <sergej.goerzen@gmail.com>
* This file is part of OmiLAXR.MetaXR.
*/
using System;
using OmiLAXR.Components;
using UnityEngine;

namespace OmiLAXR.MetaXR.Components
{
    [DisallowMultipleComponent]
    [AddComponentMenu("OmiLAXR / Config / [MetaXR] Eye Calibrator")]
    public sealed class MetaXREyeCalibrator : EyeCalibrator
    {
        private void Awake()
        {
            if (!OVRPlugin.eyeTrackingSupported)
            {
                Debug.LogWarning("Eye tracking not supported on this device.");
                enabled = false;
                return;
            }
            // Tipp: OVRManager → Permissions Request On Startup: Eye Tracking anhaken
            // (oder Permission manuell anfragen)
        }
        
        protected override void Start()
        {
            var headsetType = OVRPlugin.GetSystemHeadsetType();
            
            IsEyeTrackingAvailable = headsetType == OVRPlugin.SystemHeadset.Meta_Quest_Pro ||
                                      headsetType == OVRPlugin.SystemHeadset.Meta_Link_Quest_Pro;

            // Todo: what to do with OVRPlugin.eyeTrackingSupported?
            IsCalibrated = OVRPlugin.eyeTrackingEnabled;
            NeedsCalibration = !OVRPlugin.eyeTrackingEnabled;
            
            base.Start();
        }

        protected override void OnStartCalibration(Action<bool> successCallback, Action<string> failedCallback)
        {
            var res = OVRPlugin.StartEyeTracking();
            successCallback(res);
        }

        protected override void OnStoppedCalibration(Action callback)
        {
            OVRPlugin.StopEyeTracking();
        }
    }
}