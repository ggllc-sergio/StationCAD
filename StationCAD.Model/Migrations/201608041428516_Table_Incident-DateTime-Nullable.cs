namespace StationCAD.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Table_IncidentDateTimeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Incidents", "DispatchedDateTime", c => c.DateTime());
            AlterColumn("dbo.IncidentNotes", "EnteredDateTime", c => c.DateTime());
            AlterColumn("dbo.IncidentUnits", "EnteredDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IncidentUnits", "EnteredDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.IncidentNotes", "EnteredDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Incidents", "DispatchedDateTime", c => c.DateTime(nullable: false));
        }
    }
}
