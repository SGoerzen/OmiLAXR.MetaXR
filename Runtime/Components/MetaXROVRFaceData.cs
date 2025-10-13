/*
* SPDX-License-Identifier: AGPL-3.0-or-later
* Copyright (C) 2025 Sergej Görzen <sergej.goerzen@gmail.com>
* This file is part of OmiLAXR.MetaXR.
*/
using OmiLAXR.Components;
using OmiLAXR.Types;
using UnityEngine;

namespace OmiLAXR.MetaXR.Components
{
    public sealed class MetaXROVRFaceData : FaceData
    {
        private float W(OVRFaceExpressions.FaceExpression expr) => ((int)expr < Weights.Length) ? this[(int)expr] : 0f;
        private float C(OVRFaceExpressions.FaceExpression expr) => ((int)expr < Confidences.Length) ? GetConfidence((int)expr) : 1f;

        private void Add(FaceActionUnit au, OVRFaceExpressions.FaceExpression expr)
        {
            AuValues[au] = W(expr);
            AuConfidences[au] = C(expr);
        }

        private void Add(FaceActionUnit au, OVRFaceExpressions.FaceExpression left, OVRFaceExpressions.FaceExpression right)
        {
            AuValues[au] = Mathf.Max(W(left), W(right));
            AuConfidences[au] = Mathf.Max(C(left), C(right));
        }

        public MetaXROVRFaceData(float[] weights, float[] confidences) : base(weights, confidences)
        {
            if (weights == null || confidences == null)
                return;
            // Map Action Units
            Add(FaceActionUnit.AU1_InnerBrowRaiser, OVRFaceExpressions.FaceExpression.InnerBrowRaiserL, OVRFaceExpressions.FaceExpression.InnerBrowRaiserR);
            Add(FaceActionUnit.AU2_OuterBrowRaiser, OVRFaceExpressions.FaceExpression.OuterBrowRaiserL, OVRFaceExpressions.FaceExpression.OuterBrowRaiserR);
            Add(FaceActionUnit.AU4_BrowLowerer, OVRFaceExpressions.FaceExpression.BrowLowererL, OVRFaceExpressions.FaceExpression.BrowLowererR);
            Add(FaceActionUnit.AU5_UpperLidRaiser, OVRFaceExpressions.FaceExpression.UpperLidRaiserL, OVRFaceExpressions.FaceExpression.UpperLidRaiserR);
            Add(FaceActionUnit.AU6_CheekRaiser, OVRFaceExpressions.FaceExpression.CheekRaiserL, OVRFaceExpressions.FaceExpression.CheekRaiserR);
            Add(FaceActionUnit.AU7_LidTightener, OVRFaceExpressions.FaceExpression.LidTightenerL, OVRFaceExpressions.FaceExpression.LidTightenerR);
            Add(FaceActionUnit.AU9_NoseWrinkler, OVRFaceExpressions.FaceExpression.NoseWrinklerL, OVRFaceExpressions.FaceExpression.NoseWrinklerR);
            Add(FaceActionUnit.AU10_UpperLipRaiser, OVRFaceExpressions.FaceExpression.UpperLipRaiserL, OVRFaceExpressions.FaceExpression.UpperLipRaiserR);
            Add(FaceActionUnit.AU12_LipCornerPuller, OVRFaceExpressions.FaceExpression.LipCornerPullerL, OVRFaceExpressions.FaceExpression.LipCornerPullerR);
            Add(FaceActionUnit.AU15_LipCornerDepressor, OVRFaceExpressions.FaceExpression.LipCornerDepressorL, OVRFaceExpressions.FaceExpression.LipCornerDepressorR);
            Add(FaceActionUnit.AU17_ChinRaiser, OVRFaceExpressions.FaceExpression.ChinRaiserT, OVRFaceExpressions.FaceExpression.ChinRaiserB);
            Add(FaceActionUnit.AU20_LipStretcher, OVRFaceExpressions.FaceExpression.LipStretcherL, OVRFaceExpressions.FaceExpression.LipStretcherR);
            Add(FaceActionUnit.AU23_LipTightener, OVRFaceExpressions.FaceExpression.LipTightenerL, OVRFaceExpressions.FaceExpression.LipTightenerR);
            Add(FaceActionUnit.AU24_LipPressor, OVRFaceExpressions.FaceExpression.LipPressorL, OVRFaceExpressions.FaceExpression.LipPressorR);
            Add(FaceActionUnit.AU25_LipsPart, OVRFaceExpressions.FaceExpression.LipsToward);
            Add(FaceActionUnit.AU26_JawDrop, OVRFaceExpressions.FaceExpression.JawDrop);
            Add(FaceActionUnit.AU45_Blink, OVRFaceExpressions.FaceExpression.EyesClosedL, OVRFaceExpressions.FaceExpression.EyesClosedR);
        }
    }
}
