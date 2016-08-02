namespace StationCAD.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddResponses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserOrganizationAffiliationID = c.Int(nullable: false),
                        IncidentID = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        StatusUpdateDateTime = c.DateTime(nullable: false),
                        ETA = c.Time(nullable: false, precision: 7),
                        XCoordinate = c.String(),
                        YCoordinate = c.String(),
                        Gelocation = c.String(),
                        CreateUser = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateUser = c.String(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Incidents", t => t.IncidentID, cascadeDelete: false)
                .ForeignKey("dbo.UserOrganizationAffiliations", t => t.UserOrganizationAffiliationID, cascadeDelete: false)
                .Index(t => t.UserOrganizationAffiliationID)
                .Index(t => t.IncidentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responses", "UserOrganizationAffiliationID", "dbo.UserOrganizationAffiliations");
            DropForeignKey("dbo.Responses", "IncidentID", "dbo.Incidents");
            DropIndex("dbo.Responses", new[] { "IncidentID" });
            DropIndex("dbo.Responses", new[] { "UserOrganizationAffiliationID" });
            DropTable("dbo.Responses");
        }
    }
}
