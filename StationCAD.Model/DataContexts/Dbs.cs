
using System.Data.Entity;

namespace StationCAD.Model.DataContexts
{

    public class StationCADDb : DbContext
    {
        public StationCADDb()
            : base("StationCAD_Web")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static StationCADDb Create()
        {
            return new StationCADDb();
        }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<IncidentAddress> IncidentAddresses { get; set; }
        public DbSet<IncidentNote> IncidentNotes { get; set; }
        public DbSet<IncidentEvent> IncidentEvents { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UserOrganizationAffiliation> UserOrganizationAffiliations { get; set; }
        public DbSet<OrganizationUserNotifcation> OrganizationUserNotifcations { get; set; }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Address> Addresses { get; set; }
        
    }
}
