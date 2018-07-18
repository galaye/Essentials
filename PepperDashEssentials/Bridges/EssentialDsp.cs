﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PepperDash.Essentials.Core;
using PepperDash.Essentials.Core.Config;
using PepperDash.Essentials.DM;
using PepperDash.Core;
using PepperDash.Essentials.Core.Routing;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.EthernetCommunication;
using Crestron.SimplSharpPro.DM;

namespace PepperDash.Essentials {
	public class EssentialDsp : PepperDash.Core.Device {
		public EssentialDspProperties Properties;
		public List<BridgeApiEisc> BridgeApiEiscs;
		private PepperDash.Essentials.Devices.Common.DSP.QscDsp Dsp;
		private EssentialDspApiMap ApiMap = new EssentialDspApiMap();
		public EssentialDsp(string key, string name, JToken properties)
			: base(key, name) {
			Properties = JsonConvert.DeserializeObject<EssentialDspProperties>(properties.ToString());


			}
		public override bool CustomActivate() {
			// Create EiscApis 
			try
			{
				foreach (var device in DeviceManager.AllDevices) 
				{
					if (device.Key == this.Properties.connectionDeviceKey) 
					{
						Debug.Console(2, "deviceKey {0} Matches", device.Key);
						Dsp = DeviceManager.GetDeviceForKey(device.Key) as PepperDash.Essentials.Devices.Common.DSP.QscDsp;
						break;
					} 
					else 	
					{
						Debug.Console(2, "deviceKey {0} doesn't match", device.Key);
						
					}
				}
				if (Properties.EiscApiIpids != null && Dsp != null)
				{
					foreach (string Ipid in Properties.EiscApiIpids)
					{
						var ApiEisc = new BridgeApiEisc(Ipid);
						Debug.Console(2, "Connecting EiscApi {0} to {1}", ApiEisc.Ipid, Dsp.Name);
						ushort x = 1;
						foreach (var channel in Dsp.LevelControlPoints)
						{
							//var QscChannel = channel.Value as PepperDash.Essentials.Devices.Common.DSP.QscDspLevelControl;
							Debug.Console(2, "QscChannel {0} connect", x);
							
							var QscChannel = channel.Value as IBasicVolumeWithFeedback;
							QscChannel.MuteFeedback.LinkInputSig(ApiEisc.Eisc.BooleanInput[ApiMap.channelMuteToggle[x]]);
							QscChannel.VolumeLevelFeedback.LinkInputSig(ApiEisc.Eisc.UShortInput[ApiMap.channelVolume[x]]);
							ApiEisc.Eisc.SetSigTrueAction(ApiMap.channelMuteToggle[x], () =>  QscChannel.MuteToggle());
							ApiEisc.Eisc.SetUShortSigAction(ApiMap.channelVolume[x], u => QscChannel.SetVolume(u));
							ApiEisc.Eisc.SetStringSigAction(ApiMap.presetString, s => Dsp.RunPreset(s));
							x++;

						}

					}
				}

				


				Debug.Console(2, "Name {0} Activated", this.Name);
				return true;
				}
			catch (Exception e) {
				Debug.Console(2, "BRidge {0}", e);
				return false;
				}
			}
		}
	public class EssentialDspProperties {
		public string connectionDeviceKey;
		public string[] EiscApiIpids;


		}


	public class EssentialDspApiMap {
		public ushort presetString = 2000;
		public Dictionary<uint, ushort> channelMuteToggle;
		public Dictionary<uint, ushort> channelVolume;
		public Dictionary<uint, ushort> TxOnlineStatus;
		public Dictionary<uint, ushort> RxOnlineStatus;
		public Dictionary<uint, ushort> TxVideoSyncStatus;
		public Dictionary<uint, ushort> InputNames;
		public Dictionary<uint, ushort> OutputNames;
		public Dictionary<uint, ushort> OutputRouteNames;

		public EssentialDspApiMap() {
			channelMuteToggle = new Dictionary<uint, ushort>();
			channelVolume = new Dictionary<uint, ushort>();
			for (uint x = 1; x <= 100; x++) {
				uint tempNum = x;

				channelMuteToggle[tempNum] = (ushort)(tempNum + 400);
				channelVolume[tempNum] = (ushort)(tempNum + 200);
	
				}
			}
		}
	}
	