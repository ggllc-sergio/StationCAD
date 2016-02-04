namespace StationCAD.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Level1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IncidentAddresses", "CreateUser", c => c.String(nullable: false));
            AlterColumn("dbo.IncidentAddresses", "LastUpdateUser", c => c.String(nullable: false));
            AlterColumn("dbo.IncidentEvents", "CreateUser", c => c.String(nullable: false));
            AlterColumn("dbo.IncidentEvents", "LastUpdateUser", c => c.String(nullable: false));
            AlterColumn("dbo.IncidentNotes", "CreateUser", c => c.String(nullable: false));
            AlterColumn("dbo.IncidentNotes", "LastUpdateUser", c => c.String(nullable: false));
            AlterColumn("dbo.Incidents", "CreateUser", c => c.String(nullable: false));
            AlterColumn("dbo.Incidents", "LastUpdateUser", c => c.String(nullable: false));
            AlterColumn("dbo.OrganizationAddresses", "CreateUser", c => c.String(nullable: false));
            AlterColumn("dbo.OrganizationAddresses", "LastUpdateUser", c => c.String(nullable: false));
            AlterColumn("dbo.Organizations", "CreateUser", c => c.String(nullable: false));
            AlterColumn("dbo.Organizations", "LastUpdateUser", c => c.String(nullable: false));
            AlterColumn("dbo.OrganizationUserNotifcations", "CreateUser", c => c.String(nullable: false));
            AlterColumn("dbo.OrganizationUserNotifcations", "LastUpdateUser", c => c.String(nullable: false));
            AlterColumn("dbo.UserOrganizationAffiliations", "CreateUser", c => c.String(nullable: false));
            AlterColumn("dbo.UserOrganizationAffiliations", "LastUpdateUser", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "CreateUser", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "LastUpdateUser", c => c.String(nullable: false));
            AlterColumn("dbo.UserAddresses", "CreateUser", c => c.String(nullable: false));
            AlterColumn("dbo.UserAddresses", "LastUpdateUser", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAddresses", "LastUpdateUser", c => c.String());
            AlterColumn("dbo.UserAddresses", "CreateUser", c => c.String());
            AlterColumn("dbo.Users", "LastUpdateUser", c => c.String());
            AlterColumn("dbo.Users", "CreateUser", c => c.String());
            AlterColumn("dbo.UserOrganizationAffiliations", "LastUpdateUser", c => c.String());
            AlterColumn("dbo.UserOrganizationAffiliations", "CreateUser", c => c.String());
            AlterColumn("dbo.OrganizationUserNotifcations", "LastUpdateUser", c => c.String());
            AlterColumn("dbo.OrganizationUserNotifcations", "CreateUser", c => c.String());
            AlterColumn("dbo.Organizations", "LastUpdateUser", c => c.String());
            AlterColumn("dbo.Organizations", "CreateUser", c => c.String());
            AlterColumn("dbo.OrganizationAddresses", "LastUpdateUser", c => c.String());
            AlterColumn("dbo.OrganizationAddresses", "CreateUser", c => c.String());
            AlterColumn("dbo.Incidents", "LastUpdateUser", c => c.String());
            AlterColumn("dbo.Incidents", "CreateUser", c => c.String());
            AlterColumn("dbo.IncidentNotes", "LastUpdateUser", c => c.String());
            AlterColumn("dbo.IncidentNotes", "CreateUser", c => c.String());
            AlterColumn("dbo.IncidentEvents", "LastUpdateUser", c => c.String());
            AlterColumn("dbo.IncidentEvents", "CreateUser", c => c.String());
            AlterColumn("dbo.IncidentAddresses", "LastUpdateUser", c => c.String());
            AlterColumn("dbo.IncidentAddresses", "CreateUser", c => c.String());
        }
    }
}
