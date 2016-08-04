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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2"));


            // 1-to-many
            modelBuilder.Entity<IncidentAddress>()
                .HasRequired<Incident>(i => i.Incident)
                .WithMany(i => i.LocationAddresses);

            // 1-to-many
            modelBuilder.Entity<IncidentNote>()
                .HasRequired<Incident>(i => i.Incident)
                .WithMany(i => i.Notes);

            // 1-to-many
            modelBuilder.Entity<IncidentUnit>()
                .HasRequired<Incident>(i => i.Incident)
                .WithMany(i => i.Units);

            // 1-to-many
            modelBuilder.Entity<OrganizationAddress>()
                .HasRequired<Organization>(o => o.Organization)
                .WithMany(o => o.Addresses)
                .WillCascadeOnDelete(false);


        }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<IncidentAddress> IncidentAddresses { get; set; }
        public DbSet<IncidentNote> IncidentNotes { get; set; }
        public DbSet<IncidentUnit> IncidentUnits { get; set; }

        public DbSet<Response> Responses { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<UserOrganizationAffiliation> UserOrganizationAffiliations { get; set; }
        public DbSet<OrganizationUserNotifcation> OrganizationUserNotifcations { get; set; }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationAddress> OrganizationAddresses { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();

            var currentTime = DateTime.Now;

            foreach (var entry in entries)
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

