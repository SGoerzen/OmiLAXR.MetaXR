/*
* SPDX-License-Identifier: AGPL-3.0-or-later
* Copyright (C) 2025 Sergej Görzen <sergej.goerzen@gmail.com>
* This file is part of OmiLAXR.MetaXR.
*/
using System.ComponentModel;
using Oculus.Interaction.Locomotion;
using OmiLAXR.Listeners;
using UnityEngine;

namespace OmiLAXR.MetaXR.Listeners
{
    [AddComponentMenu("OmiLAXR / 1) Listeners / [MetaXR] <TeleportInteractor> Objects Listener"),
     Description("Provides all <TeleportInteractor> components to pipeline.")]
    public sealed class MetaXRTeleportListener : Listener
    {
        public override void StartListening()
        {
            var gos = FindObjects<TeleportInteractor>();
            Found(gos);
        }
    }
}