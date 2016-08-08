namespace StationCAD.Model.IdentityDBMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        UserClaimId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.UserClaimId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        SecurityQuestion = c.String(),
                        SecurityAnswer = c.String(),
                        AccountEmail = c.String(),
                        IdentificationNumber = c.String(),
                        NotificationEmail = c.String(),
                        NotificationCellPhone = c.String(),
                        NotifcationPushMobile = c.String(),
                        NotifcationPushBrowser = c.String(),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SecurityUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.SecurityUser_Id)
                .Index(t => t.SecurityUser_Id);
            
            CreateTable(
                "dbo.UserAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrimaryMailing = c.Boolean(nullable: false),
                        PrimaryBilling = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        Building = c.String(),
                        Development = c.String(),
                        OccupantName = c.String(),
                        Number = c.String(),
                        Street = c.String(),
                        XStreet1 = c.String(),
                        XStreet2 = c.String(),
                        County = c.String(),
                        Municipality = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        XCoordinate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YCoordinate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PlaceID = c.String(),
                        MapUrl = c.String(),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserMobileDevices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MobileNumber = c.String(),
                        Carrier = c.Int(nullable: false),
                        EnablePush = c.Boolean(nullable: false),
                        EnableSMS = c.Boolean(nullable: false),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfiles", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserMobileDeviceOrganizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Organization_Id = c.Int(),
                        UserDevice_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .ForeignKey("dbo.UserMobileDevices", t => t.UserDevice_Id)
                .Index(t => t.Organization_Id)
                .Index(t => t.UserDevice_Id);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Tag = c.String(nullable: false),
                        ContactPhone = c.String(nullable: false),
                        ContactFAX = c.String(),
                        ContactEmail = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        BillingAddress_Id = c.Int(),
                        MailingAddress_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrganizationAddresses", t => t.BillingAddress_Id)
                .ForeignKey("dbo.OrganizationAddresses", t => t.MailingAddress_Id)
                .Index(t => t.BillingAddress_Id)
                .Index(t => t.MailingAddress_Id);
            
            CreateTable(
                "dbo.OrganizationAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MailingAddress = c.Boolean(nullable: false),
                        BillingAddress = c.Boolean(nullable: false),
                        PrimaryMailing = c.Boolean(nullable: false),
                        PrimaryBilling = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        Building = c.String(),
                        Development = c.String(),
                        OccupantName = c.String(),
                        Number = c.String(),
                        Street = c.String(),
                        XStreet1 = c.String(),
                        XStreet2 = c.String(),
                        County = c.String(),
                        Municipality = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        XCoordinate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YCoordinate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PlaceID = c.String(),
                        MapUrl = c.String(),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Organization_Id = c.Int(),
                        Organization_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id1)
                .Index(t => t.Organization_Id)
                .Index(t => t.Organization_Id1);
            
            CreateTable(
                "dbo.Incidents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrganizationId = c.Int(nullable: false),
                        CADIdentifier = c.Int(nullable: false),
                        Title = c.String(),
                        IncidentIdentifier = c.Guid(nullable: false),
                        DispatchedDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ConsoleID = c.String(),
                        IncidentTypeCode = c.String(),
                        IncidentSubTypeCode = c.String(),
                        FinalIncidentTypeCode = c.String(),
                        FinalIncidentSubTypeCode = c.String(),
                        LocalIncidentID = c.String(),
                        EventID = c.String(),
                        LocalXRefID = c.String(),
                        LocalBoxArea = c.String(),
                        CallerName = c.String(),
                        CallerAddress = c.String(),
                        CallerPhone = c.String(),
                        LocalUnits = c.String(),
                        RAWCADIncidentData = c.String(),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.IncidentAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IncidentLocationType = c.Int(nullable: false),
                        RawAddress = c.String(),
                        FormattedAddress = c.String(),
                        GeoPartialMatch = c.Boolean(nullable: false),
                        PrimaryMailing = c.Boolean(nullable: false),
                        PrimaryBilling = c.Boolean(nullable: false),
                        Type = c.Int(nullable: false),
                        Building = c.String(),
                        Development = c.String(),
                        OccupantName = c.String(),
                        Number = c.String(),
                        Street = c.String(),
                        XStreet1 = c.String(),
                        XStreet2 = c.String(),
                        County = c.String(),
                        Municipality = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                        XCoordinate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YCoordinate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PlaceID = c.String(),
                        MapUrl = c.String(),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Incident_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Incidents", t => t.Incident_Id)
                .Index(t => t.Incident_Id);
            
            CreateTable(
                "dbo.IncidentNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnteredDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Author = c.String(),
                        Message = c.String(),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Incident_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Incidents", t => t.Incident_Id)
                .Index(t => t.Incident_Id);
            
            CreateTable(
                "dbo.IncidentUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnteredDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UnitID = c.String(),
                        Disposition = c.String(),
                        Comment = c.String(),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Incident_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Incidents", t => t.Incident_Id)
                .Index(t => t.Incident_Id);
            
            CreateTable(
                "dbo.OrganizationUserAffiliations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CurrentOrganization_Id = c.Int(),
                        CurrentUser_Id = c.String(maxLength: 128),
                        UserProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.CurrentOrganization_Id)
                .ForeignKey("dbo.User", t => t.CurrentUser_Id)
                .ForeignKey("dbo.UserProfiles", t => t.UserProfile_Id)
                .Index(t => t.CurrentOrganization_Id)
                .Index(t => t.CurrentUser_Id)
                .Index(t => t.UserProfile_Id);
            
            CreateTable(
                "dbo.OrganizationUserNotifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotifcationType = c.Int(nullable: false),
                        MessageTitle = c.String(),
                        MessageBody = c.String(nullable: false),
                        Sent = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Affilitation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrganizationUserAffiliations", t => t.Affilitation_Id)
                .Index(t => t.Affilitation_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserProfiles", "SecurityUser_Id", "dbo.User");
            DropForeignKey("dbo.OrganizationUserAffiliations", "UserProfile_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.OrganizationUserNotifications", "Affilitation_Id", "dbo.OrganizationUserAffiliations");
            DropForeignKey("dbo.OrganizationUserAffiliations", "CurrentUser_Id", "dbo.User");
            DropForeignKey("dbo.OrganizationUserAffiliations", "CurrentOrganization_Id", "dbo.Organizations");
            DropForeignKey("dbo.UserMobileDeviceOrganizations", "UserDevice_Id", "dbo.UserMobileDevices");
            DropForeignKey("dbo.UserMobileDeviceOrganizations", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "MailingAddress_Id", "dbo.OrganizationAddresses");
            DropForeignKey("dbo.IncidentUnits", "Incident_Id", "dbo.Incidents");
            DropForeignKey("dbo.Incidents", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.IncidentNotes", "Incident_Id", "dbo.Incidents");
            DropForeignKey("dbo.IncidentAddresses", "Incident_Id", "dbo.Incidents");
            DropForeignKey("dbo.Organizations", "BillingAddress_Id", "dbo.OrganizationAddresses");
            DropForeignKey("dbo.OrganizationAddresses", "Organization_Id1", "dbo.Organizations");
            DropForeignKey("dbo.OrganizationAddresses", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.UserMobileDevices", "User_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.UserAddresses", "User_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropIndex("dbo.OrganizationUserNotifications", new[] { "Affilitation_Id" });
            DropIndex("dbo.OrganizationUserAffiliations", new[] { "UserProfile_Id" });
            DropIndex("dbo.OrganizationUserAffiliations", new[] { "CurrentUser_Id" });
            DropIndex("dbo.OrganizationUserAffiliations", new[] { "CurrentOrganization_Id" });
            DropIndex("dbo.IncidentUnits", new[] { "Incident_Id" });
            DropIndex("dbo.IncidentNotes", new[] { "Incident_Id" });
            DropIndex("dbo.IncidentAddresses", new[] { "Incident_Id" });
            DropIndex("dbo.Incidents", new[] { "OrganizationId" });
            DropIndex("dbo.OrganizationAddresses", new[] { "Organization_Id1" });
            DropIndex("dbo.OrganizationAddresses", new[] { "Organization_Id" });
            DropIndex("dbo.Organizations", new[] { "MailingAddress_Id" });
            DropIndex("dbo.Organizations", new[] { "BillingAddress_Id" });
            DropIndex("dbo.UserMobileDeviceOrganizations", new[] { "UserDevice_Id" });
            DropIndex("dbo.UserMobileDeviceOrganizations", new[] { "Organization_Id" });
            DropIndex("dbo.UserMobileDevices", new[] { "User_Id" });
            DropIndex("dbo.UserAddresses", new[] { "User_Id" });
            DropIndex("dbo.UserProfiles", new[] { "SecurityUser_Id" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropTable("dbo.OrganizationUserNotifications");
            DropTable("dbo.OrganizationUserAffiliations");
            DropTable("dbo.IncidentUnits");
            DropTable("dbo.IncidentNotes");
            DropTable("dbo.IncidentAddresses");
            DropTable("dbo.Incidents");
            DropTable("dbo.OrganizationAddresses");
            DropTable("dbo.Organizations");
            DropTable("dbo.UserMobileDeviceOrganizations");
            DropTable("dbo.UserMobileDevices");
            DropTable("dbo.UserAddresses");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.User");
            DropTable("dbo.UserRole");
            DropTable("dbo.Role");
        }
    }
}
