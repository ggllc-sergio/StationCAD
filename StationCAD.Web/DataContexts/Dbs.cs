using Microsoft.AspNet.Identity.EntityFramework;
using StationCAD.Model;
using StationCAD.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StationCAD.Web.DataContexts
{
    public class IdentityDb : IdentityDbContext<ApplicationUser>
    {
        public IdentityDb()
            : base("StationCAD_Web", throwIfV1Schema: false)
        {
        }

        public static IdentityDb Create()
        {
            return new IdentityDb();
        }
    }


    public class StationCADDb : DbContext
    {
        public StationCADDb()
            : base("StationCAD_Web")
        {
        }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<IncidentAddress> IncidentAddresses { get; set; }
        public DbSet<IncidentNote> IncidentNotes { get; set; }
        public DbSet<IncidentEvent> IncidentEvents { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<OrganizationUser> OrganizationUsers { get; set; }
        public DbSet<OrganizationUserNotifcation> OrganizationUserNotifcations { get; set; }
        
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Address> Addresses { get; set; }



    }
}