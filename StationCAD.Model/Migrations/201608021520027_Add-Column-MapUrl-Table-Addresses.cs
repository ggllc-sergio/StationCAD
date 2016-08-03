namespace StationCAD.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnMapUrlTableAddresses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IncidentAddresses", "MapUrl", c => c.String());
            AddColumn("dbo.OrganizationAddresses", "MapUrl", c => c.String());
            AddColumn("dbo.UserAddresses", "MapUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAddresses", "MapUrl");
            DropColumn("dbo.OrganizationAddresses", "MapUrl");
            DropColumn("dbo.IncidentAddresses", "MapUrl");
        }
    }
}
