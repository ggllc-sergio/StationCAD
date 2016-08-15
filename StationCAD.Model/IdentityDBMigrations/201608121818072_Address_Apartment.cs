namespace StationCAD.Model.IdentityDBMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Address_Apartment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAddresses", "Apartment", c => c.String());
            AddColumn("dbo.OrganizationAddresses", "Apartment", c => c.String());
            AddColumn("dbo.IncidentAddresses", "Apartment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IncidentAddresses", "Apartment");
            DropColumn("dbo.OrganizationAddresses", "Apartment");
            DropColumn("dbo.UserAddresses", "Apartment");
        }
    }
}
