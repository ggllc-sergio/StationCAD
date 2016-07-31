using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using Newtonsoft.Json;

namespace StationCAD.Model
{
     public abstract class DispatchEvent : BaseModel
    {

    }


    public class ChesCoPAEventMessage : DispatchEvent
    {
        public ChesCoPAEventMessage()
        {
            this.Units = new List<UnitEntry>();
            this.Comments = new List<EventComment>();
            this.ChesCoData = new Model.ChesterCountyData();
        }

        protected ChesterCountyData ChesCoData { get; set; }

        public int OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }

        public ReportType ReportType { get; set;
        }
        public string Title { get; set; }

        [DisplayName("Call Time")]
        public string CallTime { get; set; }

        [DisplayName("Event")]
        public string Event { get; set; }

        [DisplayName("Event Type Code")]
        public string EventTypeCode { get; set; }

        [DisplayName("Event SubType Code")]
        public string EventSubTypeCode { get; set; }

        [DisplayName("ESZ")]
        public string ESZ { get; set; }

        [DisplayName("Beat")]
        public string Beat { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Cross Street")]
        public string CrossStreet { get; set; }

        [DisplayName("Location Information")]
        public string LocationInformaiton { get; set; }

        [DisplayName("Development")]
        public string Development { get; set; }

        [DisplayName("Municipality")]
        public string Municipality { get; set; }

        public Municipality LocationMunicipality
        {
            get
            {
                if (Municipality.Length > 0)
                {
                    Municipality item = ChesCoData.Municipalities.Where(x => x.Abbreviation == Municipality).FirstOrDefault();
                    if (item != null)
                        return item;
                }
                return null;
            }
        }
       
        public ICollection<GeoLocation> GeoLocations { get; set; }

        [DisplayName("Caller Information")]
        public string CallerInformation { get; set; }

        [DisplayName("Caller Name")]
        public string CallerName { get; set; }

        [DisplayName("Caller Phone Number")]
        public string CallerPhoneNumber { get; set; }

        [DisplayName("Alt Phone Number")]
        public string AltPhoneNumber { get; set; }

        [DisplayName("Caller Address")]
        public string CallerAddress { get; set; }

        [DisplayName("Caller Source")]
        public string CallerSource { get; set; }

        public ICollection<UnitEntry> Units { get; set; }

        [DisplayName("Event Comments")]
        public ICollection<EventComment> Comments { get; set; }
    }

    public class UnitEntry
    {
        public string Unit { get; set; }
        public string Disposition { get; set; }
        public string TimeStamp { get; set; }
    }

    public class EventComment
    {
        public string TimeStamp { get; set; }
        public string Comment { get; set; }
    }
}
