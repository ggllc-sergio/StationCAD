namespace StationCAD.Model.Migrations
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
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
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
                        DispatchedDateTime = c.DateTime(nullable: false),
                        ConsoleID = c.String(),
                        IncidentTypeCode = c.String(),
                        IncidentSubTypeCode = c.String(),
                        FinalIncidentTypeCode = c.String(),
                        FinalIncidentSubTypeCode = c.String(),
                        LocalIncidentID = c.String(),
                        LocalXRefID = c.String(),
                        LocalBoxArea = c.String(),
                        CallerName = c.String(),
                        CallerAddress = c.String(),
                        CallerPhone = c.String(),
                        LocalUnits = c.String(),
                        RAWCADIncidentData = c.String(),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.IncidentNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnteredDateTime = c.DateTime(nullable: false),
                        Author = c.String(),
                        Message = c.String(),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
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
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
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
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
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
                        EnteredDateTime = c.DateTime(nullable: false),
                        UnitID = c.String(),
                        Disposition = c.String(),
                        Comment = c.String(),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
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
                        UserOrganizationAffiliationId = c.Int(nullable: false),
                        NotifcationType = c.Int(nullable: false),
                        MessageTitle = c.String(),
                        MessageBody = c.String(nullable: false),
                        Sent = c.DateTime(nullable: false),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.UserOrganizationAffiliations", t => t.UserOrganizationAffiliationId, cascadeDelete: true)
                .Index(t => t.UserOrganizationAffiliationId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserOrganizationAffiliations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        IdentificationNumber = c.String(),
                        NotificationEmail = c.String(),
                        NotificationCellPhone = c.String(),
                        NotifcationPushMobile = c.String(),
                        NotifcationPushBrowser = c.String(),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserMobileDevices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        MobileNumber = c.String(),
                        Carrier = c.Int(nullable: false),
                        EnablePush = c.Boolean(nullable: false),
                        EnableSMS = c.Boolean(nullable: false),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserMobileDeviceOrganizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserMobileDeviceID = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .ForeignKey("dbo.UserMobileDevices", t => t.UserMobileDeviceID, cascadeDelete: true)
                .Index(t => t.UserMobileDeviceID)
                .Index(t => t.OrganizationID);
            
            CreateTable(
                "dbo.UserAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
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
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAddresses", "UserID", "dbo.Users");
            DropForeignKey("dbo.OrganizationUserNotifcations", "UserOrganizationAffiliationId", "dbo.UserOrganizationAffiliations");
            DropForeignKey("dbo.UserOrganizationAffiliations", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrganizationUserNotifcations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserMobileDeviceOrganizations", "UserMobileDeviceID", "dbo.UserMobileDevices");
            DropForeignKey("dbo.UserMobileDeviceOrganizations", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.UserMobileDevices", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserOrganizationAffiliations", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.IncidentAddresses", "Incident_Id", "dbo.Incidents");
            DropForeignKey("dbo.IncidentUnits", "Incident_Id", "dbo.Incidents");
            DropForeignKey("dbo.Organizations", "MailingAddress_Id", "dbo.OrganizationAddresses");
            DropForeignKey("dbo.Incidents", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "BillingAddress_Id", "dbo.OrganizationAddresses");
            DropForeignKey("dbo.OrganizationAddresses", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.IncidentNotes", "Incident_Id", "dbo.Incidents");
            DropIndex("dbo.UserAddresses", new[] { "UserID" });
            DropIndex("dbo.UserMobileDeviceOrganizations", new[] { "OrganizationID" });
            DropIndex("dbo.UserMobileDeviceOrganizations", new[] { "UserMobileDeviceID" });
            DropIndex("dbo.UserMobileDevices", new[] { "UserID" });
            DropIndex("dbo.UserOrganizationAffiliations", new[] { "OrganizationId" });
            DropIndex("dbo.UserOrganizationAffiliations", new[] { "UserId" });
            DropIndex("dbo.OrganizationUserNotifcations", new[] { "User_Id" });
            DropIndex("dbo.OrganizationUserNotifcations", new[] { "UserOrganizationAffiliationId" });
            DropIndex("dbo.IncidentUnits", new[] { "Incident_Id" });
            DropIndex("dbo.OrganizationAddresses", new[] { "Organization_Id" });
            DropIndex("dbo.Organizations", new[] { "MailingAddress_Id" });
            DropIndex("dbo.Organizations", new[] { "BillingAddress_Id" });
            DropIndex("dbo.IncidentNotes", new[] { "Incident_Id" });
            DropIndex("dbo.Incidents", new[] { "OrganizationId" });
            DropIndex("dbo.IncidentAddresses", new[] { "Incident_Id" });
            DropTable("dbo.UserAddresses");
            DropTable("dbo.UserMobileDeviceOrganizations");
            DropTable("dbo.UserMobileDevices");
            DropTable("dbo.Users");
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
