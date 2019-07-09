﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Crestron.SimplSharp;

using PepperDash.Core;
using PepperDash.Essentials.Devices.Common.Codec;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PepperDash.Essentials.Devices.Common.VideoCodec.ZoomRoom
{
    public enum eZoomRoomResponseType
    {
        zEvent,
        zStatus,
        zConfiguration,
        zCommand
    }

    public abstract class NotifiableObject : INotifyPropertyChanged
    {
            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            protected void NotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            #endregion
    }

    /// <summary>
    /// Used to track the current status of a ZoomRoom
    /// </summary>
    public class ZoomRoomStatus
    {
        public zStatus.Login Login { get; set; }
        public zStatus.SystemUnit SystemUnit { get; set; }
        public zStatus.Phonebook Phonebook { get; set; }
        public zStatus.Call Call { get; set; }
        public zStatus.Capabilities Capabilities { get; set; }
        public zStatus.Sharing Sharing { get; set; }
        public zStatus.NumberOfScreens NumberOfScreens { get; set; }
        public zStatus.Layout Layout { get; set; }
        public zStatus.Video Video { get; set; }
        public zStatus.CameraShare CameraShare { get; set; }
        public List<zStatus.AudioVideoInputOutputLineItem> AudioInputs { get; set; }
        public List<zStatus.AudioVideoInputOutputLineItem> AudioOuputs { get; set; }
        public List<zStatus.AudioVideoInputOutputLineItem> Cameras { get; set; }

        public ZoomRoomStatus()
        {
            Login = new zStatus.Login();
            SystemUnit = new zStatus.SystemUnit();
            Phonebook = new zStatus.Phonebook();
            Call = new zStatus.Call();
            Capabilities = new zStatus.Capabilities();
            Sharing = new zStatus.Sharing();
            NumberOfScreens = new zStatus.NumberOfScreens();
            Layout = new zStatus.Layout();
            Video = new zStatus.Video();
            CameraShare = new zStatus.CameraShare();
            AudioInputs = new List<zStatus.AudioVideoInputOutputLineItem>();
            AudioOuputs = new List<zStatus.AudioVideoInputOutputLineItem>();
            Cameras = new List<zStatus.AudioVideoInputOutputLineItem>();
        }
    }

    /// <summary>
    /// Used to track the current configuration of a ZoomRoom
    /// </summary>
    public class ZoomRoomConfiguration
    {
        public zConfiguration.Call Call { get; set; }
        public zConfiguration.Audio Audio { get; set; }
        public zConfiguration.Video Video { get; set; }
        public zConfiguration.Client Client { get; set; }

        public ZoomRoomConfiguration()
        {
            Call = new zConfiguration.Call();
            Audio = new zConfiguration.Audio();
            Video = new zConfiguration.Video();
            Client = new zConfiguration.Client();
        }
    }

    /// <summary>
    /// Represents a response from a ZoomRoom system
    /// </summary>
    public class Response
    {
        public Status Status { get; set; }
        public bool Sync { get; set; }
        [JsonProperty("topKey")]
        public string TopKey { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }

        public Response()
        {
            Status = new Status();
        }
    }

    public class Status
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
    }


    /// <summary>
    /// zStatus class stucture
    /// </summary>
    public class zStatus
    {
        public class Login
        {
            [JsonProperty("ZAAPI Release")]
            public string ZAAPIRelease { get; set; }
            [JsonProperty("Zoom Room Release")]
            public string ZoomRoomRelease { get; set; }
        }

        public class SystemUnit
        {
            [JsonProperty("email")]
            public string Email { get; set; }
            [JsonProperty("login_type")]
            public string LoginType { get; set; }
            [JsonProperty("meeting_number")]
            public string MeetingNumber { get; set; }
            [JsonProperty("platform")]
            public string Platform { get; set; }
            [JsonProperty("room_info")]
            public RoomInfo RoomInfo { get; set; }
            [JsonProperty("room_version")]
            public string RoomVersion { get; set; }

            public SystemUnit()
            {
                RoomInfo = new RoomInfo();
            }
        }

        public class RoomInfo
        {
            [JsonProperty("account_email")]
            public string AccountEmail { get; set; }
            [JsonProperty("display_version")]
            public string DisplayVersion { get; set; }
            [JsonProperty("is_auto_answer_enabled")]
            public bool AutoAnswerIsEnabled { get; set; }
            [JsonProperty("is_auto_answer_selected")]
            public bool AutoAnswerIsSelected { get; set; }
            [JsonProperty("room_name")]
            public string RoomName { get; set; }
        }

        public class CloudPbxInfo
        {
            [JsonProperty("company_number")]
            public string CompanyNumber { get; set; }
            [JsonProperty("extension")]
            public string Extension { get; set; }
            [JsonProperty("isValid")]
            public bool IsValid { get; set; }
        }

        public enum ePresence
        {
            PRESENCE_OFFLINE,
            PRESENCE_ONLINE,
            PRESENCE_AWAY,
            PRESENCE_BUSY,
            PRESENCE_DND
        }

        public class Contact
        {
            [JsonProperty("avatarURL")]
            public string AvatarURL { get; set; }
            [JsonProperty("cloud_pbx_info")]
            public CloudPbxInfo CloudPbxInfo { get; set; }
            [JsonProperty("email")]
            public string Email { get; set; }
            [JsonProperty("firstName")]
            public string FirstName { get; set; }
            [JsonProperty("index")]
            public int Index { get; set; }
            [JsonProperty("isLegacy")]
            public bool IsLegacy { get; set; }
            [JsonProperty("isZoomRoom")]
            public bool IsZoomRoom { get; set; }
            [JsonProperty("jid")]
            public string Jid { get; set; }
            [JsonProperty("lastName")]
            public string LastName { get; set; }
            [JsonProperty("onDesktop")]
            public bool OnDesktop { get; set; }
            [JsonProperty("onMobile")]
            public bool OnMobile { get; set; }
            [JsonProperty("phoneNumber")]
            public string PhoneNumber { get; set; }
            [JsonProperty("presence")]
            public ePresence Presence { get; set; }
            [JsonProperty("presence_status")]
            public int PresenceStatus { get; set; }
            [JsonProperty("screenName")]
            public string ScreenName { get; set; }
            [JsonProperty("sip_phone_number")]
            public string SipPhoneNumber { get; set; }


            public Contact()
            {
                CloudPbxInfo = new CloudPbxInfo();
            }
        }

        /// <summary>
        /// Used to be able to inplement IInvitableContact on DirectoryContact
        /// </summary>
        public class ZoomDirectoryContact : DirectoryContact, IInvitableContact
        {

        }

        public class Phonebook
        {
            [JsonProperty("Contacts")]
            public List<Contact> Contacts { get; set; }

            public Phonebook()
            {
                Contacts = new List<Contact>();
            }

            /// <summary>
            /// Converts from zStatus.Contact types to generic directory items
            /// </summary>
            /// <returns></returns>
            public static CodecDirectory ConvertZoomContactsToGeneric(List<Contact> zoomContacts)
            {
                var directory = new Codec.CodecDirectory();

                var folders = new List<Codec.DirectoryItem>();

                var roomFolder = new DirectoryFolder();

                var contactFolder = new DirectoryFolder();

                var contacts = new List<DirectoryItem>();

                // Check if there are any zoom rooms
                var zoomRooms = zoomContacts.FindAll(c => c.IsZoomRoom);

                if (zoomRooms.Count > 0)
                {
                    // If so, setup a rooms and contacts folder and add them.
                    roomFolder.Name = "Rooms";
                    roomFolder.FolderId = "rooms";

                    contactFolder.Name = "Contacts";
                    contactFolder.FolderId = "contacts";

                    folders.Add(roomFolder);
                    folders.Add(contactFolder);

                    directory.AddFoldersToDirectory(folders);
                }

                try
                {
                    if (zoomContacts.Count > 0)
                    {
                        foreach (Contact c in zoomContacts)
                        {
                            var contact = new ZoomDirectoryContact();

                            contact.Name = c.ScreenName;
                            contact.ContactId = c.Jid;

                            if (folders.Count > 0)
                            {
                                if (c.IsZoomRoom)
                                    contact.FolderId = roomFolder.FolderId;
                                else
                                    contact.FolderId = contactFolder.FolderId;
                            }

                            contacts.Add(contact);
                        }

                        directory.AddContactsToDirectory(contacts);
                    }
                }
                catch (Exception e)
                {
                    Debug.Console(1, "Error converting Zoom Phonebook results to generic: {0}", e);
                }
                
                return directory;
            }
        }

        public enum eCallStatus
        {
            UNKNOWN,
            NOT_IN_MEETING,
            CONNECTING_MEETING,
            IN_MEETING,
            LOGGED_OUT
        }

        public class ClosedCaption
        {
            public bool Available { get; set; }
        }

        public class Call : NotifiableObject
        {
            private eCallStatus _status;
            private List<zCommand.ListParticipant> _participants;

            public bool IsInCall;

            public eCallStatus Status 
            {
                get
                {
                    return _status;
                }
                set
                {
                    if (value != _status)
                    {
                        _status = value;
                        IsInCall = _status == eCallStatus.IN_MEETING || _status == eCallStatus.CONNECTING_MEETING;
                        NotifyPropertyChanged("Status");
                    }
                }
            }
            public ClosedCaption ClosedCaption { get; set; }
            public List<zCommand.ListParticipant> Participants 
            {
                get
                {
                    return _participants;
                }
                set
                {
                    _participants = value;
                    NotifyPropertyChanged("Participants");
                }
            }
            public zEvent.SharingState Sharing { get; set; }

            public CallRecordInfo CallRecordInfo { get; set; }

            public zCommand.InfoResult Info { get; set; }

            public Call()
            {
                ClosedCaption = new ClosedCaption();
                Participants = new List<zCommand.ListParticipant>();
                Sharing = new zEvent.SharingState();
                CallRecordInfo = new CallRecordInfo();
                Info = new zCommand.InfoResult();
            }
        }

        public class Capabilities
        {
            public bool aec_Setting_Stored_In_ZR { get; set; }
            public bool can_Dtmf_For_Invite_By_Phone { get; set; }
            public bool can_Mute_On_Entry { get; set; }
            public bool can_Ringing_In_Pstn_Call { get; set; }
            public bool can_Switch_To_Specific_Camera { get; set; }
            public bool is_Airhost_Disabled { get; set; }
            public bool pstn_Call_In_Local_resentation { get; set; }
            public bool support_Claim_Host { get; set; }
            public bool support_Out_Room_Display { get; set; }
            public bool support_Pin_And_Spotlight { get; set; }
            public bool supports_Audio_Checkup { get; set; }
            public bool supports_CheckIn { get; set; }
            public bool supports_Cloud_PBX { get; set; }
            public bool supports_Encrypted_Connection { get; set; }
            public bool supports_Expel_User_Permanently { get; set; }
            public bool supports_H323_DTMF { get; set; }
            public bool supports_Hdmi_Cec_Control { get; set; }
            public bool supports_Highly_Reverberant_Room { get; set; }
            public bool supports_Loading_Contacts_Dynamically { get; set; }
            public bool supports_Loading_Participants_Dynamically { get; set; }
            public bool supports_Mic_Record_Test { get; set; }
            public bool supports_Multi_Share { get; set; }
            public bool supports_ShareCamera { get; set; }
            public bool supports_Share_For_Floating_And_Content_Only { get; set; }
            public bool supports_Sip_Call_out { get; set; }
            public bool supports_Software_Audio_Processing { get; set; }
            public bool supports_Web_Settings_Push { get; set; }
        }

        public class Sharing : NotifiableObject
        {
            private string _dispState;
            private string _password;

            public string directPresentationPairingCode { get; set; }
            /// <summary>
            /// Laptop client sharing key
            /// </summary>
            public string directPresentationSharingKey { get; set; }
            public string dispState 
            {
                get
                {
                    return _dispState;
                }
                set
                {
                    if (value != _dispState)
                    {
                        _dispState = value;
                        NotifyPropertyChanged("dispState");
                    }
                }
            }
            public bool isAirHostClientConnected { get; set; }
            public bool isBlackMagicConnected { get; set; }
            public bool isBlackMagicDataAvailable { get; set; }
            public bool isDirectPresentationConnected { get; set; }
            public bool isSharingBlackMagic { get; set; }
            /// <summary>
            /// IOS Airplay code
            /// </summary>
            public string password 
                            {
                get
                {
                    return _password;
                }
                set
                {
                    if (value != _password)
                    {
                        _password = value;
                        NotifyPropertyChanged("password");
                    }
                }
            }
            public string serverName { get; set; }
            public string wifiName { get; set; }
        }

        public class NumberOfScreens
        {
            [JsonProperty("NumberOfCECScreens")]
            public int NumOfCECScreens { get; set; }
            [JsonProperty("NumberOfScreens")]
            public int NumOfScreens { get; set; }
        }

        /// <summary>
        /// AudioInputLine/AudioOutputLine/VideoCameraLine list item
        /// </summary>
        public class AudioVideoInputOutputLineItem
        {
            public string Alias { get; set; }
            public string Name { get; set; }
            public bool Selected { get; set; }
            public bool combinedDevice { get; set; }
            public string id { get; set; }
            public bool manuallySelected { get; set; }
            public int numberOfCombinedDevices { get; set; }
            public int ptzComId { get; set; }
        }

        public class Video
        {
            public bool Optimizable { get; set; }
        }

        public class CameraShare : NotifiableObject
        {
            private bool _canControlCamera;
            private bool _isSharing;

            [JsonProperty("can_Control_Camera")]
            public bool CanControlCamera
            {
                get
                {
                    return _canControlCamera;
                }
                set
                {
                    if (value != _canControlCamera)
                    {
                        _canControlCamera = value;
                        NotifyPropertyChanged("CanControlCamera");
                    }
                }
            }
            public string id { get; set; }
            public bool is_Mirrored { get; set; }
            [JsonProperty("is_Sharing")]
            public bool IsSharing 
            {
                get
                {
                    return _isSharing;
                }
                set
                {
                    if (value != _isSharing)
                    {
                        _isSharing = value;
                        NotifyPropertyChanged("IsSharing");
                    }
                }
            }
            public int pan_Tilt_Speed { get; set; }

        }

        public class Layout
        {
            public bool can_Adjust_Floating_Video { get; set; }
            public bool can_Switch_Floating_Share_Content { get; set; }
            public bool can_Switch_Share_On_All_Screens { get; set; }
            public bool can_Switch_Speaker_View { get; set; }
            public bool can_Switch_Wall_View { get; set; }
            public bool is_In_First_Page { get; set; }
            public bool is_In_Last_Page { get; set; }
            public bool is_supported { get; set; }
            public int video_Count_In_Current_Page { get; set; }
            public string video_type { get; set; }
        }

        public class CallRecordInfo
        {
            public bool canRecord { get; set; }
            public bool emailRequired { get; set; }
            public bool amIRecording { get; set; }
            public bool meetingIsBeingRecorded { get; set; }
        }
    }
    
    /// <summary>
    /// zEvent Class Structure
    /// </summary>
    public class zEvent
    {
        public class NeedWaitForHost
        {
            public bool Wait { get; set; }
        }

        public class IncomingCallIndication
        {
            public string callerJID { get; set; }
            public string calleeJID { get; set; }
            public string meetingID { get; set; }
            public string password { get; set; }
            public string meetingOption { get; set; }
            public int MeetingNumber { get; set; }
            public string callerName { get; set; }
            public string avatarURL { get; set; }
            public int lifeTime { get; set; }
            public bool accepted { get; set; }
        }

        public class CallConnectError
        {
            public int error_code { get; set; }
            public string error_message { get; set; }
        }

        public class CallDisconnect
        {
            public bool Successful
            {
                get
                {
                    return success == "on";
                }
            }

            public string success { get; set; }
        }

        public class Layout
        {
            public bool Sharethumb { get; set; }
        }

        public class Call
        {
            public Layout Layout { get; set; }
        }

        public class Client
        {
            public Call Call { get; set; }
        }

        public enum eSharingState
        {
            None,
            Connecting,
            Sending,
            Receiving,
            Send_Receiving
        }

        public class SharingState : NotifiableObject
        {
            private bool _paused;
            private eSharingState _state;

            public bool IsSharing;

            [JsonProperty("paused")]
            public bool Paused 
            {
                get
                {
                    return _paused;
                }
                set
                {
                    if (value != _paused)
                    {
                        _paused = value;
                        NotifyPropertyChanged("Paused");
                    }
                }
            }
            [JsonProperty("state")]
            public eSharingState State
            {
                get
                {
                    return _state;
                }
                set
                {
                    if (value != _state)
                    {
                        _state = value;
                        IsSharing = _state == eSharingState.Sending;
                        NotifyPropertyChanged("State");
                    }
                }
            }
        }

        public class PinStatusOfScreenNotification
        {
            [JsonProperty("can_be_pinned")]
            public bool CanBePinned { get; set; }
            [JsonProperty("can_pin_share")]
            public bool CanPinShare { get; set; }
            [JsonProperty("pinned_share_source_id")]
            public int PinnedShareSourceId { get; set; }
            [JsonProperty("pinned_user_id")]
            public int PinnedUserId { get; set; }
            [JsonProperty("screen_index")]
            public int ScreenIndex { get; set; }
            [JsonProperty("screen_layout")]
            public int ScreenLayout { get; set; }
            [JsonProperty("share_source_type")]
            public int ShareSourceType { get; set; }
            [JsonProperty("why_cannot_pin_share")]
            public string WhyCannotPinShare { get; set; }
        }
    }
    
    /// <summary>
    /// zConfiguration class structure
    /// </summary>
    public class zConfiguration
    {
        public class Sharing
        {
            [JsonProperty("optimize_video_sharing")]
            public bool OptimizeVideoSharing { get; set; }
        }

        public class Camera
        {
            public bool Mute { get; set; }
        }

        public class Microphone : NotifiableObject
        {
            private bool _mute;

            public bool Mute 
            { 
                get
                {
                    return _mute;   
                }
                set
                {
                    if(value != _mute)
                    {
                        _mute = value;
                        NotifyPropertyChanged("Mute");
                    }
                }
            }
        }

        public enum eLayoutStyle
        {
            Gallery,
            Speaker,
            Strip,
            ShareAll
        }

        public enum eLayoutSize
        {
            Off,
            Size1,
            Size2,
            Size3,
            Strip
        }

        public enum eLayoutPosition
        {
            Center,
            Up,
            Right,
            UpRight,
            Down,
            DownRight,
            Left,
            UpLeft,
            DownLeft
        }

        public class Layout
        {
            public bool ShareThumb { get; set; }
            public eLayoutStyle Style { get; set; }
            public eLayoutSize Size { get; set; }
            public eLayoutPosition Position { get; set; }
        }

        public class Lock
        {
            public bool Enable { get; set; }
        }

        public class ClosedCaption
        {
            public bool Visible { get; set; }
            public int FontSize { get; set; }
        }

        public class MuteUserOnEntry
        {
            public bool Enable { get; set; }
        }

        public class Call
        {
            public Sharing Sharing { get; set; }
            public Camera Camera { get; set; }
            public Microphone Microphone { get; set; }
            public Layout Layout { get; set; }
            public Lock Lock { get; set; }
            public MuteUserOnEntry MuteUserOnEntry { get; set; }
            public ClosedCaption ClosedCaption { get; set; }


            public Call()
            {
                Sharing = new Sharing();
                Camera = new Camera();
                Microphone = new Microphone();
                Layout = new Layout();
                Lock = new Lock();
                MuteUserOnEntry = new MuteUserOnEntry();
                ClosedCaption = new ClosedCaption();
            }
        }

        public class Audio
        {
            public Input Input { get; set; }
            public Output Output { get; set; }

            public Audio()
            {
                Input = new Input();
                Output = new Output();
            }
        }

        public class Input : Output
        {
            [JsonProperty("reduce_reverb")]
            public bool ReduceReverb { get; set; }
        }

        public class Output : NotifiableObject
        {
            private int _volume;

            [JsonProperty("volume")]
            public int Volume
            {
                get
                {
                    return this._volume;
                }
                set
                {
                    if (value != _volume)
                    {
                        this._volume = value;
                        NotifyPropertyChanged("Volume");
                    }
                }
            }
            [JsonProperty("selectedId")]
            public string SelectedId { get; set; }
            [JsonProperty("is_sap_disabled")]
            public bool IsSapDisabled { get; set; }
        }

        public class Video : NotifiableObject
        {
            private bool _hideConfSelfVideo;

            [JsonProperty("hide_conf_self_video")]
            public bool HideConfSelfVideo 
            {
                get
                {
                    return _hideConfSelfVideo;
                }
                set
                {
                    if (value != _hideConfSelfVideo)
                    {
                        _hideConfSelfVideo = value;
                        NotifyPropertyChanged("HideConfSelfVideo");
                    }
                }
            }

            public VideoCamera Camera { get; set; }

            public Video()
            {
                Camera = new VideoCamera();
            }
        }

        public class VideoCamera : NotifiableObject
        {
            private string _selectedId;

            [JsonProperty("selectedId")]
            public string SelectedId {
                get
                {
                    return _selectedId;
                }
                set
                {
                    if (value != _selectedId)
                    {
                        _selectedId = value;
                        NotifyPropertyChanged("SelectedId");
                    }
                }
                
            }
            public bool Mirror { get; set; }
        }

        public class Client
        {
            public string appVersion { get; set; }
            public string deviceSystem { get; set; }
        }

    }

    /// <summary>
    /// zCommand class structure
    /// </summary>
    public class zCommand
    {
        public partial class BookingsListResult
        {
            [JsonProperty("accessRole")]
            public string AccessRole { get; set; }
            [JsonProperty("calendarChangeKey")]
            public string CalendarChangeKey { get; set; }
            [JsonProperty("calendarID")]
            public string CalendarId { get; set; }
            [JsonProperty("checkIn")]
            public bool CheckIn { get; set; }
            [JsonProperty("creatorEmail")]
            public string CreatorEmail { get; set; }
            [JsonProperty("creatorName")]
            public string CreatorName { get; set; }
            [JsonProperty("endTime")]
            public DateTime EndTime { get; set; }
            [JsonProperty("hostName")]
            public string HostName { get; set; }
            [JsonProperty("isInstantMeeting")]
            public bool IsInstantMeeting { get; set; }
            [JsonProperty("isPrivate")]
            public bool IsPrivate { get; set; }
            [JsonProperty("location")]
            public string Location { get; set; }
            [JsonProperty("meetingName")]
            public string MeetingName { get; set; }
            [JsonProperty("meetingNumber")]
            public string MeetingNumber { get; set; }
            [JsonProperty("scheduledFrom")]
            public string ScheduledFrom { get; set; }
            [JsonProperty("startTime")]
            public DateTime StartTime { get; set; }
            [JsonProperty("third_party")]
            public ThirdParty ThirdParty { get; set; }
        }

        /// <summary>
        /// Extracts the necessary meeting values from the Cisco bookings response ans converts them to the generic class
        /// </summary>
        /// <param name="bookings"></param>
        /// <returns></returns>
        public static List<Meeting> GetGenericMeetingsFromBookingResult(List<BookingsListResult> bookings)
        {
            var meetings = new List<Meeting>();

            if (Debug.Level > 0)
            {
                Debug.Console(1, "Meetings List:\n");
            }

            foreach (var b in bookings)
            {
                var meeting = new Meeting();

                if (b.MeetingNumber != null)
                    meeting.Id = b.MeetingNumber;
                if (b.CreatorName != null)
                    meeting.Organizer = b.CreatorName;
                if (b.MeetingName != null)
                    meeting.Title = b.MeetingName;
                //if (b.Agenda != null)
                //    meeting.Agenda = b.Agenda.Value;
                if (b.StartTime != null)
                    meeting.StartTime = b.StartTime;
                if (b.EndTime != null)
                    meeting.EndTime = b.EndTime;
                if (b.IsPrivate != null)
                    meeting.Privacy = b.IsPrivate ? eMeetingPrivacy.Private : eMeetingPrivacy.Public;

                // No meeting.Calls data exists for Zoom Rooms.  Leaving out for now.

                meetings.Add(meeting);

                if (Debug.Level > 0)
                {
                    Debug.Console(1, "Title: {0}, ID: {1}, Organizer: {2}", meeting.Title, meeting.Id, meeting.Organizer);
                    Debug.Console(1, "    Start Time: {0}, End Time: {1}, Duration: {2}", meeting.StartTime, meeting.EndTime, meeting.Duration);
                    Debug.Console(1, "    Joinable: {0}\n", meeting.Joinable);
                }
            }

            meetings.OrderBy(m => m.StartTime);

            return meetings;
        }
        
        public class HandStatus
        {
            [JsonProperty("is_raise_hand")]
            public bool IsRaiseHand { get; set; }
            [JsonProperty("optimize_vis_validideo_sharing")]
            public string IsValid { get; set; }
            [JsonProperty("time_stamp")]
            public string TimeStamp { get; set; }
        }

        public class ListParticipant
        {
            [JsonProperty("audio_status state")]
            public string AudioStatusState { get; set; }
            [JsonProperty("audio_status type")]
            public string AudioStatusType { get; set; }
            [JsonProperty("avatar_url")]
            public string AvatarUrl { get; set; }
            [JsonProperty("camera_status am_i_controlling")]
            public bool CameraStatusAmIControlling { get; set; }
            [JsonProperty("camera_status can_i_request_control")]
            public bool CameraStatusCanIRequestConrol { get; set; }
            [JsonProperty("camera_status can_move_camera")]
            public bool CameraStatusCanMoveCamera { get; set; }
            [JsonProperty("camera_status can_switch_camera")]
            public bool CameraStatusCanSwitchCamera { get; set; }
            [JsonProperty("camera_status can_zoom_camera")]
            public bool CameraStatusCanZoomCamera { get; set; }
            [JsonProperty("can_edit_closed_caption")]
            public bool CanEditClosedCaption { get; set; }
            [JsonProperty("can_record")]
            public bool CanRecord { get; set; }
            [JsonProperty("event")]
            public string Event { get; set; }
            [JsonProperty("hand_status")]
            public HandStatus HandStatus { get; set; }
            [JsonProperty("isCohost")]
            public bool IsCohost { get; set; }
            [JsonProperty("is_client_support_closed_caption")]
            public bool IsClientSupportClosedCaption { get; set; }
            [JsonProperty("is_client_support_coHost")]
            public bool IsClientSupportCoHost { get; set; }
            [JsonProperty("is_host")]
            public bool IsHost { get; set; }
            [JsonProperty("is_myself")]
            public bool IsMyself { get; set; }
            [JsonProperty("is_recording")]
            public bool IsRecording { get; set; }
            [JsonProperty("is_video_can_mute_byHost")]
            public bool IsVideoCanMuteByHost { get; set; }
            [JsonProperty("is_video_can_unmute_byHost")]
            public bool IsVideoCanUnmuteByHost { get; set; }
            [JsonProperty("local_recording_disabled")]
            public bool LocalRecordingDisabled { get; set; }
            [JsonProperty("user_id")]
            public int UserId { get; set; }
            [JsonProperty("user_name")]
            public string UserName { get; set; }
            [JsonProperty("user_type")]
            public string UserType { get; set; }
            [JsonProperty("video_status has_source")]
            public bool VideoStatusHasSource { get; set; }
            [JsonProperty("video_status is_receiving")]
            public bool VideoStatusIsReceiving { get; set; }
            [JsonProperty("video_status is_sending")]
            public bool VideoStatusIsSending { get; set; }

            public ListParticipant()
            {
                HandStatus = new HandStatus();
            }
        }

        public class CallinCountryList
        {
            public int code { get; set; }
            public string display_number { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string number { get; set; }
        }

        public class CalloutCountryList
        {
            public int code { get; set; }
            public string display_number { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string number { get; set; }
        }

        public class TollFreeCallinList
        {
            public int code { get; set; }
            public string display_number { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string number { get; set; }
        }

        public class Info
        {
            public List<CallinCountryList> callin_country_list { get; set; }
            public List<CalloutCountryList> callout_country_list { get; set; }
            public List<TollFreeCallinList> toll_free_callin_list { get; set; }
        }

        public class ThirdParty
        {
            public string h323_address { get; set; }
            public string meeting_number { get; set; }
            public string service_provider { get; set; }
            public string sip_address { get; set; }
        }

        public class MeetingListItem
        {
            public string accessRole { get; set; }
            public string calendarChangeKey { get; set; }
            public string calendarID { get; set; }
            public bool checkIn { get; set; }
            public string creatorEmail { get; set; }
            public string creatorName { get; set; }
            public string endTime { get; set; }
            public string hostName { get; set; }
            public bool isInstantMeeting { get; set; }
            public bool isPrivate { get; set; }
            public string location { get; set; }
            public string meetingName { get; set; }
            public string meetingNumber { get; set; }
            public string scheduledFrom { get; set; }
            public string startTime { get; set; }
            public ThirdParty third_party { get; set; }

            public MeetingListItem()
            {
                third_party = new ThirdParty();
            }
        }

        public class InfoResult
        {
            public Info Info { get; set; }
            public bool am_i_original_host { get; set; }
            public string default_callin_country { get; set; }
            public string dialIn { get; set; }
            public string international_url { get; set; }
            public string invite_email_content { get; set; }
            public string invite_email_subject { get; set; }
            public bool is_callin_country_list_available { get; set; }
            public bool is_calling_room_system_enabled { get; set; }
            public bool is_toll_free_callin_list_available { get; set; }
            public bool is_view_only { get; set; }
            public bool is_waiting_room { get; set; }
            public bool is_webinar { get; set; }
            public string meeting_id { get; set; }
            public MeetingListItem meeting_list_item { get; set; }
            public string meeting_password { get; set; }
            public string meeting_type { get; set; }
            public int my_userid { get; set; }
            public int participant_id { get; set; }
            public string real_meeting_id { get; set; }
            public string schedule_option { get; set; }
            public string schedule_option2 { get; set; }
            public string support_callout_type { get; set; }
            public string toll_free_number { get; set; }
            public string user_type { get; set; }

            public InfoResult()
            {
                Info = new Info();
                meeting_list_item = new MeetingListItem();
            }
        }

        public class Phonebook
        {
            public List<zStatus.Contact> Contacts { get; set; }
            public int Limit { get; set; }
            public int Offset { get; set; }
        }
    }
}