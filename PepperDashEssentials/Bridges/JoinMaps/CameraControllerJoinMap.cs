﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using PepperDash.Essentials.Core;

namespace PepperDash.Essentials.Bridges
{
    /// <summary>
    /// Join map for CameraBase devices
    /// </summary>
    public class CameraControllerJoinMap : JoinMapBase
    {
        public const string IsOnline = "IsOnline";
        public const string PowerOff = "PowerOff";
        public const string PowerOn = "PowerOn";
        public const string TiltUp = "TiltUp";
        public const string TiltDown = "TiltDown";
        public const string PanLeft = "PanLeft";
        public const string PanRight = "PanRight";
        public const string ZoomIn = "ZoomIn";
        public const string ZoomOut = "ZoomOut";
        public const string PresetRecallStart = "PresetRecallStart";
        public const string PresetSaveStart = "PresetSaveStart";
        public const string PresetLabelStart = "PresetReacllStgart";
        public const string NumberOfPresets = "NumberOfPresets";
        public const string CameraModeAuto = "CameraModeAuto";
        public const string CameraModeManual = "CameraModeManual";
        public const string CameraModeOff = "CameraModeOff";
        public const string SupportsCameraModeAuto = "SupportsCameraModeAuto";
        public const string SupportsCameraModeOff = "SupportsCameraModeOff";
        public const string SupportsPresets = "SupportsPresets";

        public CameraControllerJoinMap()
        {
            Joins.Add(TiltDown, new JoinMetadata() 
            { JoinNumber = 1, Label = "Tilt Up", JoinCapabilities = eJoinCapabilities.FromSIMPL, JoinSpan = 1, JoinType = eJoinType.Digital });
            Joins.Add(TiltDown, new JoinMetadata() 
                { JoinNumber = 2, Label = "Tilt Down", JoinCapabilities = eJoinCapabilities.FromSIMPL, JoinSpan = 1, JoinType = eJoinType.Digital });
            Joins.Add(PanLeft, new JoinMetadata() 
                { JoinNumber = 3, Label = "Pan Left", JoinCapabilities = eJoinCapabilities.FromSIMPL, JoinSpan = 1, JoinType = eJoinType.Digital });
            Joins.Add(PanRight, new JoinMetadata() 
                { JoinNumber = 4, Label = "Pan Right", JoinCapabilities = eJoinCapabilities.FromSIMPL, JoinSpan = 1, JoinType = eJoinType.Digital });
            Joins.Add(ZoomIn, new JoinMetadata() 
                { JoinNumber = 5, Label = "Zoom In", JoinCapabilities = eJoinCapabilities.FromSIMPL, JoinSpan = 1, JoinType = eJoinType.Digital });
            Joins.Add(ZoomOut, new JoinMetadata() 
                { JoinNumber = 6, Label = "Zoom Out", JoinCapabilities = eJoinCapabilities.FromSIMPL, JoinSpan = 1, JoinType = eJoinType.Digital });

            Joins.Add(PowerOn, new JoinMetadata() 
                { JoinNumber = 7, Label = "Power On", JoinCapabilities = eJoinCapabilities.ToFromSIMPL, JoinSpan = 1, JoinType = eJoinType.Digital });
            Joins.Add(PowerOff, new JoinMetadata() 
                { JoinNumber = 8, Label = "Power Off", JoinCapabilities = eJoinCapabilities.ToFromSIMPL, JoinSpan = 1, JoinType = eJoinType.Digital });
            Joins.Add(IsOnline, new JoinMetadata() 
                { JoinNumber = 9, Label = "Is Online", JoinCapabilities = eJoinCapabilities.ToSIMPL, JoinSpan = 1, JoinType = eJoinType.Digital });

            Joins.Add(PresetRecallStart, new JoinMetadata() 
                { JoinNumber = 11, Label = "Preset Recall Start", JoinCapabilities = eJoinCapabilities.FromSIMPL, JoinSpan = 20, JoinType = eJoinType.Digital });
            Joins.Add(PresetLabelStart, new JoinMetadata() 
                { JoinNumber = 11, Label = "Preset Label Start", JoinCapabilities = eJoinCapabilities.FromSIMPL, JoinSpan = 20, JoinType = eJoinType.Serial });

            Joins.Add(PresetSaveStart, new JoinMetadata() 
                { JoinNumber = 31, Label = "Preset Save Start", JoinCapabilities = eJoinCapabilities.FromSIMPL, JoinSpan = 20, JoinType = eJoinType.Digital });

            Joins.Add(NumberOfPresets, new JoinMetadata() 
                { JoinNumber = 11, Label = "Number of Presets", JoinCapabilities = eJoinCapabilities.FromSIMPL, JoinSpan = 1, JoinType = eJoinType.Analog });

            Joins.Add(CameraModeAuto, new JoinMetadata()
                { JoinNumber = 51, Label = "Camera Mode Auto", JoinCapabilities = eJoinCapabilities.ToFromSIMPL, JoinSpan = 1, JoinType = eJoinType.Digital });
            Joins.Add(CameraModeManual, new JoinMetadata()
                { JoinNumber = 52, Label = "Camera Mode Manual", JoinCapabilities = eJoinCapabilities.ToFromSIMPL, JoinSpan = 1, JoinType = eJoinType.Digital });
            Joins.Add(CameraModeOff, new JoinMetadata()
                { JoinNumber = 53, Label = "Camera Mode Off", JoinCapabilities = eJoinCapabilities.ToFromSIMPL, JoinSpan = 1, JoinType = eJoinType.Digital });

            Joins.Add(SupportsCameraModeAuto, new JoinMetadata()
                { JoinNumber = 55, Label = "Supports Camera Mode Auto", JoinCapabilities = eJoinCapabilities.FromSIMPL, JoinSpan = 1, JoinType = eJoinType.Digital });
            Joins.Add(SupportsCameraModeOff, new JoinMetadata()
                { JoinNumber = 56, Label = "Supports Camera Mode Off", JoinCapabilities = eJoinCapabilities.FromSIMPL, JoinSpan = 1, JoinType = eJoinType.Digital });
            Joins.Add(SupportsPresets, new JoinMetadata()
                { JoinNumber = 57, Label = "Supports Presets", JoinCapabilities = eJoinCapabilities.FromSIMPL, JoinSpan = 1, JoinType = eJoinType.Digital });
        }

        public override void OffsetJoinNumbers(uint joinStart)
        {
            var joinOffset = joinStart - 1;
         
            foreach (var join in Joins)
            {
                join.Value.JoinNumber = join.Value.JoinNumber + joinOffset;
            }

            PrintJoinMapInfo();
        }
    }
}