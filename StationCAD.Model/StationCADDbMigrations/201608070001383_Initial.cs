namespace StationCAD.Model.StationCADDbMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
                        Incident_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Incidents", t => t.Incident_Id, cascadeDelete: true)
                .Index(t => t.Incident_Id);
            
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
                        Incident_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Incidents", t => t.Incident_Id, cascadeDelete: true)
                .Index(t => t.Incident_Id);
            
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
                        Organization_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .Index(t => t.Organization_Id);
            
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
                        Incident_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Incidents", t => t.Incident_Id, cascadeDelete: true)
                .Index(t => t.Incident_Id);
            
            CreateTable(
                "dbo.OrganizationUserNotifcations",
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
                .ForeignKey("dbo.UserOrganizationAffiliations", t => t.Affilitation_Id)
                .Index(t => t.Affilitation_Id);
            
            CreateTable(
                "dbo.UserOrganizationAffiliations",
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
                        CurrentUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.CurrentOrganization_Id)
                .ForeignKey("dbo.UserProfiles", t => t.CurrentUser_Id)
                .Index(t => t.CurrentOrganization_Id)
                .Index(t => t.CurrentUser_Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
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
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
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
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        StatusUpdateDateTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ETA = c.Time(nullable: false, precision: 7),
                        XCoordinate = c.String(),
                        YCoordinate = c.String(),
                        Gelocation = c.String(),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Incident_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Incidents", t => t.Incident_Id)
                .ForeignKey("dbo.UserOrganizationAffiliations", t => t.User_Id)
                .Index(t => t.Incident_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Responses", "User_Id", "dbo.UserOrganizationAffiliations");
            DropForeignKey("dbo.Responses", "Incident_Id", "dbo.Incidents");
            DropForeignKey("dbo.OrganizationUserNotifcations", "Affilitation_Id", "dbo.UserOrganizationAffiliations");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserProfiles", "SecurityUser_Id", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.User");
            DropForeignKey("dbo.UserOrganizationAffiliations", "CurrentUser_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.UserMobileDeviceOrganizations", "UserDevice_Id", "dbo.UserMobileDevices");
            DropForeignKey("dbo.UserMobileDeviceOrganizations", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.UserMobileDevices", "User_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.UserAddresses", "User_Id", "dbo.UserProfiles");
            DropForeignKey("dbo.UserOrganizationAffiliations", "CurrentOrganization_Id", "dbo.Organizations");
            DropForeignKey("dbo.IncidentAddresses", "Incident_Id", "dbo.Incidents");
            DropForeignKey("dbo.IncidentUnits", "Incident_Id", "dbo.Incidents");
            DropForeignKey("dbo.Organizations", "MailingAddress_Id", "dbo.OrganizationAddresses");
            DropForeignKey("dbo.Incidents", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "BillingAddress_Id", "dbo.OrganizationAddresses");
            DropForeignKey("dbo.OrganizationAddresses", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.IncidentNotes", "Incident_Id", "dbo.Incidents");
            DropIndex("dbo.Responses", new[] { "User_Id" });
            DropIndex("dbo.Responses", new[] { "Incident_Id" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.UserMobileDeviceOrganizations", new[] { "UserDevice_Id" });
            DropIndex("dbo.UserMobileDeviceOrganizations", new[] { "Organization_Id" });
            DropIndex("dbo.UserMobileDevices", new[] { "User_Id" });
            DropIndex("dbo.UserAddresses", new[] { "User_Id" });
            DropIndex("dbo.UserProfiles", new[] { "SecurityUser_Id" });
            DropIndex("dbo.UserOrganizationAffiliations", new[] { "CurrentUser_Id" });
            DropIndex("dbo.UserOrganizationAffiliations", new[] { "CurrentOrganization_Id" });
            DropIndex("dbo.OrganizationUserNotifcations", new[] { "Affilitation_Id" });
            DropIndex("dbo.IncidentUnits", new[] { "Incident_Id" });
            DropIndex("dbo.OrganizationAddresses", new[] { "Organization_Id" });
            DropIndex("dbo.Organizations", new[] { "MailingAddress_Id" });
            DropIndex("dbo.Organizations", new[] { "BillingAddress_Id" });
            DropIndex("dbo.IncidentNotes", new[] { "Incident_Id" });
            DropIndex("dbo.Incidents", new[] { "OrganizationId" });
            DropIndex("dbo.IncidentAddresses", new[] { "Incident_Id" });
            DropTable("dbo.Role");
            DropTable("dbo.Responses");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaims");
            DropTable("dbo.User");
            DropTable("dbo.UserMobileDeviceOrganizations");
            DropTable("dbo.UserMobileDevices");
            DropTable("dbo.UserAddresses");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.UserOrganizationAffiliations");
            DropTable("dbo.OrganizationUserNotifcations");
            DropTable("dbo.IncidentUnits");
            DropTable("dbo.OrganizationAddresses");
            DropTable("dbo.Organizations");
            DropTable("dbo.IncidentNotes");
            DropTable("dbo.Incidents");
            DropTable("dbo.IncidentAddresses");
        }
    }
}
