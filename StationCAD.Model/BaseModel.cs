﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StationCAD.Model
{
    public abstract class BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CreateUser { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public string LastUpdateUser { get; set; }
        [Required]
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

        public string XCoordinate { get; set; }

        public string YCoordinate { get; set; }

        public string Gelocation { get; set; }
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
}
