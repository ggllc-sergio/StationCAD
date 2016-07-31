namespace StationCAD.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Level4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IncidentAddresses", "RawAddress", c => c.String());
            AddColumn("dbo.IncidentAddresses", "FormattedAddress", c => c.String());
            AddColumn("dbo.IncidentAddresses", "PlaceID", c => c.String());
            AddColumn("dbo.Incidents", "Title", c => c.String());
            AddColumn("dbo.Incidents", "IncidentSubTypeCode", c => c.String());
            AddColumn("dbo.Incidents", "FinalIncidentSubTypeCode", c => c.String());
            AddColumn("dbo.Incidents", "LocalBoxArea", c => c.String());
            AddColumn("dbo.Incidents", "LocalUnits", c => c.String());
            AddColumn("dbo.OrganizationAddresses", "PlaceID", c => c.String());
            AddColumn("dbo.UserAddresses", "PlaceID", c => c.String());
            AlterColumn("dbo.IncidentAddresses", "XCoordinate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.IncidentAddresses", "YCoordinate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrganizationAddresses", "XCoordinate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.OrganizationAddresses", "YCoordinate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.UserAddresses", "XCoordinate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.UserAddresses", "YCoordinate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.IncidentAddresses", "Gelocation");
            DropColumn("dbo.Incidents", "EnteredDateTime");
            DropColumn("dbo.Incidents", "IncidentType");
            DropColumn("dbo.Incidents", "FinalIncidentType");
            DropColumn("dbo.Incidents", "IncidentPriority");
            DropColumn("dbo.Incidents", "FinalIncidentPriority");
            DropColumn("dbo.Incidents", "LocalFireBox");
            DropColumn("dbo.Incidents", "LocalEMSBox");
            DropColumn("dbo.Incidents", "LocalPoliceBox");
            DropColumn("dbo.Incidents", "LocationGroup");
            DropColumn("dbo.Incidents", "LocationSection");
            DropColumn("dbo.OrganizationAddresses", "Gelocation");
            DropColumn("dbo.UserAddresses", "Gelocation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAddresses", "Gelocation", c => c.String());
            AddColumn("dbo.OrganizationAddresses", "Gelocation", c => c.String());
            AddColumn("dbo.Incidents", "LocationSection", c => c.String());
            AddColumn("dbo.Incidents", "LocationGroup", c => c.String());
            AddColumn("dbo.Incidents", "LocalPoliceBox", c => c.String());
            AddColumn("dbo.Incidents", "LocalEMSBox", c => c.String());
            AddColumn("dbo.Incidents", "LocalFireBox", c => c.String());
            AddColumn("dbo.Incidents", "FinalIncidentPriority", c => c.String());
            AddColumn("dbo.Incidents", "IncidentPriority", c => c.String());
            AddColumn("dbo.Incidents", "FinalIncidentType", c => c.String());
            AddColumn("dbo.Incidents", "IncidentType", c => c.String());
            AddColumn("dbo.Incidents", "EnteredDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.IncidentAddresses", "Gelocation", c => c.String());
            AlterColumn("dbo.UserAddresses", "YCoordinate", c => c.String());
            AlterColumn("dbo.UserAddresses", "XCoordinate", c => c.String());
            AlterColumn("dbo.OrganizationAddresses", "YCoordinate", c => c.String());
            AlterColumn("dbo.OrganizationAddresses", "XCoordinate", c => c.String());
            AlterColumn("dbo.IncidentAddresses", "YCoordinate", c => c.String());
            AlterColumn("dbo.IncidentAddresses", "XCoordinate", c => c.String());
            DropColumn("dbo.UserAddresses", "PlaceID");
            DropColumn("dbo.OrganizationAddresses", "PlaceID");
            DropColumn("dbo.Incidents", "LocalUnits");
            DropColumn("dbo.Incidents", "LocalBoxArea");
            DropColumn("dbo.Incidents", "FinalIncidentSubTypeCode");
            DropColumn("dbo.Incidents", "IncidentSubTypeCode");
            DropColumn("dbo.Incidents", "Title");
            DropColumn("dbo.IncidentAddresses", "PlaceID");
            DropColumn("dbo.IncidentAddresses", "FormattedAddress");
            DropColumn("dbo.IncidentAddresses", "RawAddress");
        }
    }
}
