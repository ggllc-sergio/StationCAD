
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StationCAD.Model
{
    public class Organization : BaseModel
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Tag { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string ContactPhone { get; set; }

        public string ContactFAX { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string ContactEmail { get; set; }

        public OrganizationStatus Status { get; set; }

        public OrganizationType Type { get; set; }

        public OrganizationAddress MailingAddress { get; set; }
        public OrganizationAddress BillingAddress { get; set; }

        public virtual ICollection<OrganizationAddress> Addresses { get; set; }

        public virtual ICollection<Incident> IncidentHistory { get; set; }
        
    }

    public class OrganizationAddress : Address
    {
        public virtual Organization Organization { get; set; }

        public bool MailingAddress { get; set; }
        public bool BillingAddress { get; set; }
    }

    public enum OrganizationType
    {
        Fire=1,
        EMS,
        Police,
        GovernmentOEM
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
