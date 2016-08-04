using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StationCAD.Model
{
    public abstract class BaseModel : AbstractJsonEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }

    public abstract class Address : BaseModel
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

        public decimal XCoordinate { get; set; }

        public decimal YCoordinate { get; set; }

        public string PlaceID { get; set; }

        public string MapUrl { get; set; }
    }

    public abstract class MobilePhoneNumber
    {
        [Phone]
        public string PhoneNumber { get; set; }

        public MobileCarrier Carrier { get; set; }
    }


    public abstract class IncidentNotification : AbstractJsonEntity
    {
    }

    public abstract class AbstractJsonEntity
    {
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }

    public enum MobileCarrier
    {
        [Display(Name = "AT&T")]
        ATT = 1,
        Cingular,
        Tracfone,
        [Display(Name = "Cellular One")]
        CellularOne,
        [Display(Name = "Metro PCS")]
        MetroPCS,
        Nextel,
        Sprint,
        [Display(Name = "T-Mobile")]
        TMobile,
        Verizon,
        [Display(Name = "Virgin Mobile")]
        VirginMobile
    }
}
