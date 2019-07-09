﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using Newtonsoft.Json;

using PepperDash.Core;
using PepperDash.Essentials.Devices.Common.VideoCodec.Cisco;

namespace PepperDash.Essentials.Devices.Common.VideoCodec
{
    /// <summary>
    /// Interface for camera presets
    /// </summary>
    public interface IHasCodecRoomPresets
    {
        event EventHandler<EventArgs> CodecRoomPresetsListHasChanged;

        List<CodecRoomPreset> NearEndPresets { get; }

        List<CodecRoomPreset> FarEndRoomPresets { get; }

        void CodecRoomPresetSelect(int preset);

        void CodecRoomPresetStore(int preset, string description);
    }

    public static class RoomPresets
    {
        /// <summary>
        /// Converts Cisco RoomPresets to generic CameraPresets
        /// </summary>
        /// <param name="presets"></param>
        /// <returns></returns>
        public static List<CodecRoomPreset> GetGenericPresets(List<CiscoCodecStatus.RoomPreset> presets)
        {
            var cameraPresets = new List<CodecRoomPreset>();

            if (Debug.Level > 0)
            {
                Debug.Console(1, "Presets List:");
            }

            foreach (CiscoCodecStatus.RoomPreset preset in presets)
            {
                try
                {
                    var cameraPreset = new CodecRoomPreset(UInt16.Parse(preset.id), preset.Description.Value, preset.Defined.BoolValue, true);

                    cameraPresets.Add(cameraPreset);

                    if (Debug.Level > 0)
                    {
                        Debug.Console(1, "Added Preset ID: {0}, Description: {1}, IsDefined: {2}, isDefinable: {3}", cameraPreset.ID, cameraPreset.Description, cameraPreset.Defined, cameraPreset.IsDefinable);
                    }
                }
                catch (Exception e)
                {
                    Debug.Console(2, "Unable to convert preset: {0}. Error: {1}", preset.id, e);
                }  
            }

            return cameraPresets;
        }
    }

    /// <summary>
    /// Represents a room preset on a video coded.  Typically stores camera position(s) and video routing.  Can be recalled by Far End if enabled.
    /// </summary>
    public class CodecRoomPreset
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        /// <summary>
        /// Used to store the name of the preset
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// Indicates if the preset is defined(stored) in the codec
        /// </summary>
        [JsonProperty("defined")]
        public bool Defined { get; set; }
        /// <summary>
        /// Indicates if the preset has the capability to be defined
        /// </summary>
        [JsonProperty("isDefinable")]
        public bool IsDefinable { get; set; }

        public CodecRoomPreset(int id, string description, bool def, bool isDef)
        {
            ID = id;
            Description = description;
            Defined = def;
            IsDefinable = isDef;
        }
    }
}