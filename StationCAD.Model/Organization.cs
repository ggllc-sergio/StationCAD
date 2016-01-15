using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCAD.Model
{
    public class Organization : BaseModel
    {
        public string Name { get; set; }

        public string ContactPhone { get; set; }

        public string ContactFAX { get; set; }

        public string ContactEmail { get; set; }

        public OrganizationStatus Status { get; set; }

        public ICollection<Incident> IncidentHistory { get; set; }
    }

    public enum OrganizationStatus
    {
        Active = 1,
        Inactive,
        Suspended,
        Uknown
    }
}
