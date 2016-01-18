using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Address MailingAddress { get; set; }

        public ICollection<Address> Addresses { get; set; }

        public ICollection<Incident> IncidentHistory { get; set; }
    }


    public class Address : BaseModel
    {
        public bool PrimaryMailing { get; set; }

        public bool PrimaryBilling { get; set; }

        public AddressType Type { get; set; }

        public string Building { get; set; }

        public string Development { get; set; }

        public string OccupantName { get; set; }

        public string Number { get; set; }

        public string Street { get; set; }

        public string XStreet1 { get; set; }

        public string XStreet2 { get; set; }

        public string County { get; set; }

        public string Municipality { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string XCoordinate { get; set; }

        public string YCoordinate { get; set; }

        public string Gelocation { get; set; }
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
