namespace StationCAD.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Level5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IncidentEvents", "EventNote_Id", "dbo.IncidentNotes");
            DropForeignKey("dbo.Incidents", "LocationAddress_Id", "dbo.IncidentAddresses");
            DropForeignKey("dbo.IncidentNotes", "Incident_Id", "dbo.Incidents");
            DropForeignKey("dbo.IncidentEvents", "Incident_Id", "dbo.Incidents");
            RenameTable(name: "dbo.IncidentEvents", newName: "IncidentUnits");
            DropForeignKey("dbo.OrganizationAddresses", "OrganizationID", "dbo.Organizations");
            DropIndex("dbo.IncidentUnits", new[] { "EventNote_Id" });
            DropIndex("dbo.IncidentUnits", new[] { "Incident_Id" });
            DropIndex("dbo.IncidentNotes", new[] { "Incident_Id" });
            DropIndex("dbo.Incidents", new[] { "LocationAddress_Id" });
            DropIndex("dbo.OrganizationAddresses", new[] { "OrganizationID" });
            DropIndex("dbo.OrganizationAddresses", new[] { "Organization_Id" });
            DropColumn("dbo.OrganizationAddresses", "Organization_Id");
            RenameColumn(table: "dbo.OrganizationAddresses", name: "OrganizationID", newName: "Organization_Id");
            AddColumn("dbo.IncidentAddresses", "GeoPartialMatch", c => c.Boolean(nullable: false));
            AddColumn("dbo.IncidentAddresses", "Incident_Id", c => c.Int(nullable: false));
            AddColumn("dbo.IncidentUnits", "Comment", c => c.String());
            AlterColumn("dbo.IncidentUnits", "Incident_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.IncidentNotes", "Incident_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.OrganizationAddresses", "Organization_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.IncidentAddresses", "Incident_Id");
            CreateIndex("dbo.IncidentNotes", "Incident_Id");
            CreateIndex("dbo.OrganizationAddresses", "Organization_Id");
            CreateIndex("dbo.IncidentUnits", "Incident_Id");
            AddForeignKey("dbo.IncidentAddresses", "Incident_Id", "dbo.Incidents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IncidentNotes", "Incident_Id", "dbo.Incidents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IncidentUnits", "Incident_Id", "dbo.Incidents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrganizationAddresses", "Organization_Id", "dbo.Organizations", "Id");
            DropColumn("dbo.IncidentUnits", "EventNote_Id");
            DropColumn("dbo.Incidents", "LocationAddress_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Incidents", "LocationAddress_Id", c => c.Int());
            AddColumn("dbo.IncidentUnits", "EventNote_Id", c => c.Int());
            DropForeignKey("dbo.OrganizationAddresses", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.IncidentUnits", "Incident_Id", "dbo.Incidents");
            DropForeignKey("dbo.IncidentNotes", "Incident_Id", "dbo.Incidents");
            DropForeignKey("dbo.IncidentAddresses", "Incident_Id", "dbo.Incidents");
            DropIndex("dbo.IncidentUnits", new[] { "Incident_Id" });
            DropIndex("dbo.OrganizationAddresses", new[] { "Organization_Id" });
            DropIndex("dbo.IncidentNotes", new[] { "Incident_Id" });
            DropIndex("dbo.IncidentAddresses", new[] { "Incident_Id" });
            AlterColumn("dbo.OrganizationAddresses", "Organization_Id", c => c.Int());
            AlterColumn("dbo.IncidentNotes", "Incident_Id", c => c.Int());
            AlterColumn("dbo.IncidentUnits", "Incident_Id", c => c.Int());
            DropColumn("dbo.IncidentUnits", "Comment");
            DropColumn("dbo.IncidentAddresses", "Incident_Id");
            DropColumn("dbo.IncidentAddresses", "GeoPartialMatch");
            RenameColumn(table: "dbo.OrganizationAddresses", name: "Organization_Id", newName: "OrganizationID");
            AddColumn("dbo.OrganizationAddresses", "Organization_Id", c => c.Int());
            CreateIndex("dbo.OrganizationAddresses", "Organization_Id");
            CreateIndex("dbo.OrganizationAddresses", "OrganizationID");
            CreateIndex("dbo.Incidents", "LocationAddress_Id");
            CreateIndex("dbo.IncidentNotes", "Incident_Id");
            CreateIndex("dbo.IncidentUnits", "Incident_Id");
            CreateIndex("dbo.IncidentUnits", "EventNote_Id");
            AddForeignKey("dbo.OrganizationAddresses", "OrganizationID", "dbo.Organizations", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.IncidentUnits", newName: "IncidentEvents");
            AddForeignKey("dbo.IncidentEvents", "Incident_Id", "dbo.Incidents", "Id");
            AddForeignKey("dbo.IncidentNotes", "Incident_Id", "dbo.Incidents", "Id");
            AddForeignKey("dbo.Incidents", "LocationAddress_Id", "dbo.IncidentAddresses", "Id");
            AddForeignKey("dbo.IncidentEvents", "EventNote_Id", "dbo.IncidentNotes", "Id");
        }
    }
}
