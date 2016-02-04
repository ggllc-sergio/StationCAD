using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

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
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserOrganizationAffiliation> UserOrganizationAffiliations { get; set; }
        public DbSet<OrganizationUserNotifcation> OrganizationUserNotifcations { get; set; }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationAddress> OrganizationAddresses { get; set; }

        public override int SaveChanges()
        {
            ObjectContext context = ((IObjectContextAdapter)this).ObjectContext;

            //Find all Entities that are Added/Modified that inherit from my EntityBase
            IEnumerable<ObjectStateEntry> objectStateEntries =
                from e in context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                where
                    e.IsRelationship == false &&
                    e.Entity != null &&
                    typeof(BaseModel).IsAssignableFrom(e.Entity.GetType())
                select e;

            var currentTime = DateTime.Now;

            foreach (var entry in objectStateEntries)
            {   
                var baseModel = entry.Entity as BaseModel;
                var user = HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated ? HttpContext.Current.User.Identity.Name : "Anonymous";
                if (entry.State == EntityState.Added)
                {
                    baseModel.CreateUser = user;
                    baseModel.CreateDate = currentTime;
                }
                baseModel.LastUpdateUser = user;
                baseModel.LastUpdateDate = currentTime;
            }

            return base.SaveChanges();
        }
    }

}

