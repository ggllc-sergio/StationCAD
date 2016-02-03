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
                        LocationType = c.String(),
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
                        XCoordinate = c.String(),
                        YCoordinate = c.String(),
                        Gelocation = c.String(),
                        CreateUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(),
                        LastUpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IncidentEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnteredDateTime = c.DateTime(nullable: false),
                        UnitID = c.String(),
                        Disposition = c.String(),
                        CreateUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(),
                        LastUpdateDate = c.DateTime(nullable: false),
                        EventNote_Id = c.Int(),
                        Incident_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IncidentNotes", t => t.EventNote_Id)
                .ForeignKey("dbo.Incidents", t => t.Incident_Id)
                .Index(t => t.EventNote_Id)
                .Index(t => t.Incident_Id);
            
            CreateTable(
                "dbo.IncidentNotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EnteredDateTime = c.DateTime(nullable: false),
                        Author = c.String(),
                        Message = c.String(),
                        CreateUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(),
                        LastUpdateDate = c.DateTime(nullable: false),
                        Incident_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Incidents", t => t.Incident_Id)
                .Index(t => t.Incident_Id);
            
            CreateTable(
                "dbo.Incidents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrganizationId = c.Int(nullable: false),
                        CADIdentifier = c.Int(nullable: false),
                        IncidentIdentifier = c.Guid(nullable: false),
                        EnteredDateTime = c.DateTime(nullable: false),
                        DispatchedDateTime = c.DateTime(nullable: false),
                        ConsoleID = c.String(),
                        IncidentType = c.String(),
                        FinalIncidentType = c.String(),
                        IncidentPriority = c.String(),
                        FinalIncidentPriority = c.String(),
                        LocalIncidentID = c.String(),
                        LocalXRefID = c.String(),
                        LocalFireBox = c.String(),
                        LocalEMSBox = c.String(),
                        LocalPoliceBox = c.String(),
                        LocationGroup = c.String(),
                        LocationSection = c.String(),
                        CallerName = c.String(),
                        CallerAddress = c.String(),
                        CallerPhone = c.String(),
                        CreateUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(),
                        LastUpdateDate = c.DateTime(nullable: false),
                        LocationAddress_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IncidentAddresses", t => t.LocationAddress_Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId)
                .Index(t => t.LocationAddress_Id);
            
            CreateTable(
                "dbo.OrganizationAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrganizationID = c.Int(nullable: false),
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
                        XCoordinate = c.String(),
                        YCoordinate = c.String(),
                        Gelocation = c.String(),
                        CreateUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(),
                        LastUpdateDate = c.DateTime(nullable: false),
                        Organization_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID, cascadeDelete: true)
                .Index(t => t.OrganizationID)
                .Index(t => t.Organization_Id);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ContactPhone = c.String(),
                        ContactFAX = c.String(),
                        ContactEmail = c.String(),
                        Status = c.Int(nullable: false),
                        CreateUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(),
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
                "dbo.OrganizationUserNotifcations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserOrganizationAffiliationId = c.Int(nullable: false),
                        NotifcationType = c.Int(nullable: false),
                        MessageTitle = c.String(),
                        MessageBody = c.String(),
                        Sent = c.DateTime(nullable: false),
                        CreateUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(),
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
                        CreateUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(),
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
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        IdentificationNumber = c.String(),
                        NotificationEmail = c.String(),
                        NotificationCellPhone = c.String(),
                        NotifcationPushMobile = c.String(),
                        NotifcationPushBrowser = c.String(),
                        CreateUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(),
                        LastUpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        XCoordinate = c.String(),
                        YCoordinate = c.String(),
                        Gelocation = c.String(),
                        CreateUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(),
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
            DropForeignKey("dbo.UserOrganizationAffiliations", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.OrganizationAddresses", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "MailingAddress_Id", "dbo.OrganizationAddresses");
            DropForeignKey("dbo.Incidents", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "BillingAddress_Id", "dbo.OrganizationAddresses");
            DropForeignKey("dbo.OrganizationAddresses", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.IncidentNotes", "Incident_Id", "dbo.Incidents");
            DropForeignKey("dbo.Incidents", "LocationAddress_Id", "dbo.IncidentAddresses");
            DropForeignKey("dbo.IncidentEvents", "Incident_Id", "dbo.Incidents");
            DropForeignKey("dbo.IncidentEvents", "EventNote_Id", "dbo.IncidentNotes");
            DropIndex("dbo.UserAddresses", new[] { "UserID" });
            DropIndex("dbo.UserOrganizationAffiliations", new[] { "OrganizationId" });
            DropIndex("dbo.UserOrganizationAffiliations", new[] { "UserId" });
            DropIndex("dbo.OrganizationUserNotifcations", new[] { "User_Id" });
            DropIndex("dbo.OrganizationUserNotifcations", new[] { "UserOrganizationAffiliationId" });
            DropIndex("dbo.Organizations", new[] { "MailingAddress_Id" });
            DropIndex("dbo.Organizations", new[] { "BillingAddress_Id" });
            DropIndex("dbo.OrganizationAddresses", new[] { "Organization_Id" });
            DropIndex("dbo.OrganizationAddresses", new[] { "OrganizationID" });
            DropIndex("dbo.Incidents", new[] { "LocationAddress_Id" });
            DropIndex("dbo.Incidents", new[] { "OrganizationId" });
            DropIndex("dbo.IncidentNotes", new[] { "Incident_Id" });
            DropIndex("dbo.IncidentEvents", new[] { "Incident_Id" });
            DropIndex("dbo.IncidentEvents", new[] { "EventNote_Id" });
            DropTable("dbo.UserAddresses");
            DropTable("dbo.Users");
            DropTable("dbo.UserOrganizationAffiliations");
            DropTable("dbo.OrganizationUserNotifcations");
            DropTable("dbo.Organizations");
            DropTable("dbo.OrganizationAddresses");
            DropTable("dbo.Incidents");
            DropTable("dbo.IncidentNotes");
            DropTable("dbo.IncidentEvents");
            DropTable("dbo.IncidentAddresses");
        }
    }
}
