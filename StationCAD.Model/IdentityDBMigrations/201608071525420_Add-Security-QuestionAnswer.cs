namespace StationCAD.Model.IdentityDBMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSecurityQuestionAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "SecurityQuestion", c => c.String());
            AddColumn("dbo.UserProfiles", "SecurityAnswer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "SecurityAnswer");
            DropColumn("dbo.UserProfiles", "SecurityQuestion");
        }
    }
}
