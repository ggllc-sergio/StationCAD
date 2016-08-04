namespace StationCAD.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseModelDateTimeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IncidentAddresses", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.IncidentAddresses", "LastUpdateDate", c => c.DateTime());
            AlterColumn("dbo.Incidents", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.Incidents", "LastUpdateDate", c => c.DateTime());
            AlterColumn("dbo.IncidentNotes", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.IncidentNotes", "LastUpdateDate", c => c.DateTime());
            AlterColumn("dbo.Organizations", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.Organizations", "LastUpdateDate", c => c.DateTime());
            AlterColumn("dbo.OrganizationAddresses", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.OrganizationAddresses", "LastUpdateDate", c => c.DateTime());
            AlterColumn("dbo.IncidentUnits", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.IncidentUnits", "LastUpdateDate", c => c.DateTime());
            AlterColumn("dbo.OrganizationUserNotifcations", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.OrganizationUserNotifcations", "LastUpdateDate", c => c.DateTime());
            AlterColumn("dbo.UserOrganizationAffiliations", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.UserOrganizationAffiliations", "LastUpdateDate", c => c.DateTime());
            AlterColumn("dbo.Users", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.Users", "LastUpdateDate", c => c.DateTime());
            AlterColumn("dbo.UserMobileDevices", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.UserMobileDevices", "LastUpdateDate", c => c.DateTime());
            AlterColumn("dbo.UserMobileDeviceOrganizations", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.UserMobileDeviceOrganizations", "LastUpdateDate", c => c.DateTime());
            AlterColumn("dbo.Responses", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.Responses", "LastUpdateDate", c => c.DateTime());
            AlterColumn("dbo.UserAddresses", "CreateDate", c => c.DateTime());
            AlterColumn("dbo.UserAddresses", "LastUpdateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAddresses", "LastUpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserAddresses", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Responses", "LastUpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Responses", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserMobileDeviceOrganizations", "LastUpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserMobileDeviceOrganizations", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserMobileDevices", "LastUpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserMobileDevices", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "LastUpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserOrganizationAffiliations", "LastUpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.UserOrganizationAffiliations", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrganizationUserNotifcations", "LastUpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrganizationUserNotifcations", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.IncidentUnits", "LastUpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.IncidentUnits", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrganizationAddresses", "LastUpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrganizationAddresses", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Organizations", "LastUpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Organizations", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.IncidentNotes", "LastUpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.IncidentNotes", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Incidents", "LastUpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Incidents", "CreateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.IncidentAddresses", "LastUpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.IncidentAddresses", "CreateDate", c => c.DateTime(nullable: false));
        }
    }
}
