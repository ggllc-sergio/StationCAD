using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace StationCAD.Model.Notifications.OneSignal
{

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DeviceList : AbstractJsonEntity
    {
        [JsonProperty(PropertyName = "total_count")]
        public int TotalCount { get; set; }
        [JsonProperty()]
        public int Offset { get; set; }
        [JsonProperty()]
        public int Limit { get; set; }
        [JsonProperty(PropertyName = "players")]
        public ICollection<Device> Devices { get; set; }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Device : AbstractJsonEntity
    {
        [JsonProperty()]
        public string Identifier { get; set; }
        [JsonProperty(PropertyName = "session_count", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int SessionCount { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Language { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string TimeZone { get; set; }
        [JsonProperty(PropertyName = "game_version", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string GameVersion { get; set; }
        [JsonProperty(PropertyName = "device_os", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string DeviceOS { get; set; }
        [JsonProperty(PropertyName = "device_type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string DeviceType { get; set; }
        [JsonProperty(PropertyName = "device_model", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string DeviceModel { get; set; }
        [JsonProperty(PropertyName = "ad_id", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AdId { get; set; }
        [JsonProperty(PropertyName = "tags", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, string> Tags { get; set; }
        [JsonProperty(PropertyName = "last_active", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string LastActive { get; set; }
        public DateTime LastActiveDateTime
        {
            get
            {
                DateTime dt = new DateTime();
                long unixTS = 0;
                long.TryParse(LastActive, out unixTS);
                if (unixTS > 0)
                    dt = UnixTimeStampToDateTime(unixTS);
                return dt;
            }
        }
        [JsonProperty(PropertyName = "amount_spent", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AmountSpent { get; set; }
        [JsonProperty(PropertyName = "created_at", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string CreatedAt { get; set; }
        public DateTime CreatedAtDateTime
        {
            get
            {
                DateTime dt = new DateTime();
                long unixTS = 0;
                long.TryParse(CreatedAt, out unixTS);
                if (unixTS > 0)
                    dt = UnixTimeStampToDateTime(unixTS);
                return dt;
            }
        }
        [JsonProperty(PropertyName = "invalid_identifier", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool InvalidIdentifier { get; set; }
        [JsonProperty(PropertyName = "badge_count", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int BadgeCount { get; set; }
        [JsonProperty(PropertyName = "notification_types", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string NotificationType { get; set; }

    }
    
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DeviceEdit : Device
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public new string Identifier { get; set; }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class PushNotificationCreate : AbstractJsonEntity
    {
        [JsonProperty(PropertyName = "app_id")]
        public string ApplicationId { get; set; }
        [JsonProperty(PropertyName = "headings", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dictionary<string, string> Headings { get; set; }
        [JsonProperty(PropertyName = "contents")]
        public Dictionary<string, string> Contents { get; set; }
        [JsonProperty(PropertyName = "isIos", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool SendiOS { get; set; }
        [JsonProperty(PropertyName = "isAndroid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool SendAndroid { get; set; }
        [JsonProperty(PropertyName = "isWP", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool SendWindowsPhone { get; set; }
        [JsonProperty(PropertyName = "isAdm", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool SendAmazon { get; set; }
        [JsonProperty(PropertyName = "isChrome", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool SendChromeExt { get; set; }
        [JsonProperty(PropertyName = "isChromeWeb", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool SendChromeWeb { get; set; }
        [JsonProperty(PropertyName = "isSafari", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool SendSafari { get; set; }
        [JsonProperty(PropertyName = "isAnyWeb", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool SendAllWeb { get; set; }
        [JsonProperty(PropertyName = "included_segments", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ICollection<string> IncludeSegments { get; set; }
        [JsonProperty(PropertyName = "excluded_segments", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ICollection<string> ExcludeSegments { get; set; }
        [JsonProperty(PropertyName = "include_player_ids", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ICollection<string> IncludeDevices { get; set; }
        [JsonProperty(PropertyName = "tags", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ICollection<PushNotificationTag> IncludeTags { get; set; }
        [JsonProperty(PropertyName = "ios_sound", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string iOSSound { get; set; }
        [JsonProperty(PropertyName = "android_sound", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AndroidSound { get; set; }
        [JsonProperty(PropertyName = "adm_sound", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AmazonSound { get; set; }
        [JsonProperty(PropertyName = "wp_sound", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string WindowsPhoneSound { get; set; }
        [JsonProperty(PropertyName = "wp_wns_sound", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Windows8_1PhoneSound { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public ICollection<KeyValuePair<string, string>> Data { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Url { get; set; }
        [JsonProperty(PropertyName = "semd_after", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SendAfter { get; set; }

    }

    [JsonObject()]
    public class PushNotificationTag : AbstractJsonEntity
    {
        public string Key { get; set; }
        public string Relation { get; set; }
        public string Value { get; set; }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class PushNotificationList : AbstractJsonEntity
    {
        [JsonProperty(PropertyName = "total_count")]
        public int TotalCount { get; set; }
        [JsonProperty()]
        public int Offset { get; set; }
        [JsonProperty()]
        public int Limit { get; set; }
        [JsonProperty()]
        public ICollection<PushNotification> Notifications { get; set; }
    }

    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class PushNotification : AbstractJsonEntity
    {

        [JsonProperty()]
        public string Id { get; set; }
        [JsonProperty()]
        public int Successful { get; set; }
        [JsonProperty()]
        public int Failed { get; set; }
        [JsonProperty()]
        public int Converted { get; set; }
        [JsonProperty()]
        public bool Cancelled { get; set; }
        [JsonProperty(PropertyName = "queued_at")]
        public long QueuedAt { get; set; }
        [JsonProperty(PropertyName = "send_after")]
        public long SendAfter { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Data { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Headings { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Contents { get; set; }
    }
    
    [JsonObject()]
    public class ItemDictionary : AbstractJsonEntity
    {
        [JsonProperty()]
        public Dictionary<string, string> Item { get; set; }
    }


}


