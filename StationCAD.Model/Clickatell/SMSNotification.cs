using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace StationCAD.Model.Clickatell
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class SMSNotification : AbstractJsonEntity
    {
        public string DispatchTime { get; set; }
        public string IncidentNumber { get; set; }
        public string TypeCode { get; set; }
        public string Location { get; set; }
        public string Municipality { get; set; }
        public string Notes { get; set; }
        public List<string> Recipients { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                //sb.AppendFormat("{0}{1}", this.DispatchTime, Environment.NewLine);
                //sb.AppendFormat("{0}{1}", this.TypeCode, Environment.NewLine);
                //sb.AppendFormat("{0}{1}", this.Location, Environment.NewLine);
                //sb.AppendFormat("{0}{1}", this.Municipality, Environment.NewLine);
                //sb.AppendFormat("{0}{1}", this.IncidentNumber, Environment.NewLine);
                sb.AppendFormat("{0}{1}", this.Notes, Environment.NewLine);
                return sb.ToString();
            }
        }

        [JsonProperty(PropertyName = "to")]
        public string[] To { get { return Recipients.ToArray(); } }
    }
}
