
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StationCAD.Model
{
    public class Organization : BaseModel
    {
        public string Name { get; set; }

        public string ContactPhone { get; set; }

        public string ContactFAX { get; set; }

        public string ContactEmail { get; set; }

        public OrganizationStatus Status { get; set; }

        public OrganizationAddress MailingAddress { get; set; }
        public OrganizationAddress BillingAddress { get; set; }

        public ICollection<OrganizationAddress> Addresses { get; set; }

        public ICollection<Incident> IncidentHistory { get; set; }
        
    }

    public class OrganizationAddress : Address
    {
        public int OrganizationID { get; set; }
        public virtual Organization Organization { get; set; }

        public bool MailingAddress { get; set; }
        public bool BillingAddress { get; set; }
    }

    public enum OrganizationStatus
    {
        Active = 1,
        Inactive,
        Suspended,
        Uknown
    }

    public enum AddressType
    {
        Office = 1,
        Headquarters,
        [Display(Name = "Main Station")]
        MainStation,
        [Display(Name = "Sub Station")]
        SubStation
    }
}
