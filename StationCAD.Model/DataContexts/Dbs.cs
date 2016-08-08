using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace StationCAD.Model.DataContexts
{
    public class IdentityDb : IdentityDbContext<User, Role, string, UserLogin, UserRole, UserClaim>
    {
        public IdentityDb()
            : base("StationCAD_Web")
        {
        }

        public static IdentityDb Create()
        {
            return new IdentityDb();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLogin>().Map(c =>
            {
                c.ToTable("UserLogin");
                c.Properties(p => new
                {
                    p.UserId,
                    p.LoginProvider,
                    p.ProviderKey
                });
            }).HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });

            // Mapping for ApiRole
            modelBuilder.Entity<Role>().Map(c =>
            {
                c.ToTable("Role");
                c.Property(p => p.Id).HasColumnName("RoleId");
                c.Properties(p => new
                {
                    p.Name
                });
            }).HasKey(p => p.Id);
            modelBuilder.Entity<Role>().HasMany(c => c.Users).WithRequired().HasForeignKey(c => c.RoleId);
            modelBuilder.Entity<UserRole>().Map(c =>
            {
                c.ToTable("UserRole");
                c.Properties(p => new
                {
                    p.UserId,
                    p.RoleId
                });
            })
            .HasKey(c => new { c.UserId, c.RoleId });


            modelBuilder.Entity<User>().Map(c =>
            {
                c.ToTable("User");
                c.Property(p => p.Id).HasColumnName("UserId");
                c.Properties(p => new
                {
                    p.AccessFailedCount,
                    p.Email,
                    p.EmailConfirmed,
                    p.PasswordHash,
                    p.PhoneNumber,
                    p.PhoneNumberConfirmed,
                    p.TwoFactorEnabled,
                    p.SecurityStamp,
                    p.LockoutEnabled,
                    p.LockoutEndDateUtc,
                    p.UserName
                });
            }).HasKey(c => c.Id);
            modelBuilder.Entity<User>().HasOptional(u => u.Profile).WithRequired(p => p.SecurityUser);
            modelBuilder.Entity<User>().HasMany(c => c.Logins).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<User>().HasMany(c => c.Claims).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<User>().HasMany(c => c.Roles).WithRequired().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<UserClaim>().Map(c =>
            {
                c.ToTable("UserClaim");
                c.Property(p => p.Id).HasColumnName("UserClaimId");
                c.Properties(p => new
                {
                    p.UserId,
                    p.ClaimValue,
                    p.ClaimType
                });
            }).HasKey(c => c.Id);
            
        }


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



            modelBuilder.Entity<UserLogin>().Map(c =>
            {
                c.ToTable("UserLogin");
                c.Properties(p => new
                {
                    p.UserId,
                    p.LoginProvider,
                    p.ProviderKey
                });
            }).HasKey(p => new { p.LoginProvider, p.ProviderKey, p.UserId });

            // Mapping for ApiRole
            modelBuilder.Entity<Role>().Map(c =>
            {
                c.ToTable("Role");
                c.Property(p => p.Id).HasColumnName("RoleId");
                c.Properties(p => new
                {
                    p.Name
                });
            }).HasKey(p => p.Id);
            modelBuilder.Entity<Role>().HasMany(c => c.Users).WithRequired().HasForeignKey(c => c.RoleId);
            modelBuilder.Entity<UserRole>().Map(c =>
            {
                c.ToTable("UserRole");
                c.Properties(p => new
                {
                    p.UserId,
                    p.RoleId
                });
            })
            .HasKey(c => new { c.UserId, c.RoleId });


            modelBuilder.Entity<User>().Map(c =>
            {
                c.ToTable("User");
                c.Property(p => p.Id).HasColumnName("UserId");
                c.Properties(p => new
                {
                    p.AccessFailedCount,
                    p.Email,
                    p.EmailConfirmed,
                    p.PasswordHash,
                    p.PhoneNumber,
                    p.PhoneNumberConfirmed,
                    p.TwoFactorEnabled,
                    p.SecurityStamp,
                    p.LockoutEnabled,
                    p.LockoutEndDateUtc,
                    p.UserName
                });
            }).HasKey(c => c.Id);
            modelBuilder.Entity<User>().HasOptional(u => u.Profile).WithRequired(p => p.SecurityUser);
            modelBuilder.Entity<User>().HasMany(c => c.Logins).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<User>().HasMany(c => c.Claims).WithOptional().HasForeignKey(c => c.UserId);
            modelBuilder.Entity<User>().HasMany(c => c.Roles).WithRequired().HasForeignKey(c => c.UserId);

        }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<IncidentAddress> IncidentAddresses { get; set; }
        public DbSet<IncidentNote> IncidentNotes { get; set; }
        public DbSet<IncidentUnit> IncidentUnits { get; set; }

        public DbSet<Response> Responses { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
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

