namespace StationCAD.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Level3 : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.IncidentAddresses", "IncidentLocationType", c => c.Int(nullable: false));
            AddColumn("dbo.IncidentAddresses", "PrimaryMailing", c => c.Boolean(nullable: false));
            AddColumn("dbo.IncidentAddresses", "PrimaryBilling", c => c.Boolean(nullable: false));
            AddColumn("dbo.IncidentAddresses", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Incidents", "IncidentTypeCode", c => c.String());
            AddColumn("dbo.Incidents", "FinalIncidentTypeCode", c => c.String());
            AddColumn("dbo.Incidents", "RAWCADIncidentData", c => c.String());
            AddColumn("dbo.Organizations", "Tag", c => c.String(nullable: false));
            AddColumn("dbo.Organizations", "Type", c => c.Int(nullable: false));
            AlterColumn("dbo.Organizations", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Organizations", "ContactPhone", c => c.String(nullable: false));
            AlterColumn("dbo.Organizations", "ContactEmail", c => c.String(nullable: false));
            AlterColumn("dbo.OrganizationUserNotifcations", "MessageBody", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false));
            DropColumn("dbo.IncidentAddresses", "LocationType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IncidentAddresses", "LocationType", c => c.String());
            DropForeignKey("dbo.UserMobileDeviceOrganizations", "UserMobileDeviceID", "dbo.UserMobileDevices");
            DropForeignKey("dbo.UserMobileDeviceOrganizations", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.UserMobileDevices", "UserID", "dbo.Users");
            DropIndex("dbo.UserMobileDeviceOrganizations", new[] { "OrganizationID" });
            DropIndex("dbo.UserMobileDeviceOrganizations", new[] { "UserMobileDeviceID" });
            DropIndex("dbo.UserMobileDevices", new[] { "UserID" });
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String());
            AlterColumn("dbo.OrganizationUserNotifcations", "MessageBody", c => c.String());
            AlterColumn("dbo.Organizations", "ContactEmail", c => c.String());
            AlterColumn("dbo.Organizations", "ContactPhone", c => c.String());
            AlterColumn("dbo.Organizations", "Name", c => c.String());
            DropColumn("dbo.Organizations", "Type");
            DropColumn("dbo.Organizations", "Tag");
            DropColumn("dbo.Incidents", "RAWCADIncidentData");
            DropColumn("dbo.Incidents", "FinalIncidentTypeCode");
            DropColumn("dbo.Incidents", "IncidentTypeCode");
            DropColumn("dbo.IncidentAddresses", "Type");
            DropColumn("dbo.IncidentAddresses", "PrimaryBilling");
            DropColumn("dbo.IncidentAddresses", "PrimaryMailing");
            DropColumn("dbo.IncidentAddresses", "IncidentLocationType");
            DropTable("dbo.UserMobileDeviceOrganizations");
            DropTable("dbo.UserMobileDevices");
        }
    }
}
