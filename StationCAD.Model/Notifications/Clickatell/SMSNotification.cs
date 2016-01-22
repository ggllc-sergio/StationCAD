using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace StationCAD.Model.Notifications.Clickatell
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class SMSNotification : AbstractJsonEntity
    {
        public List<string> Recipients { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "to")]
        public string[] To { get { return Recipients.ToArray(); } }
    }
}
