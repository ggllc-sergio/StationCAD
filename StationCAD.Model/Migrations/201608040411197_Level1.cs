namespace StationCAD.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Level1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserMobileDevices", "UserID", "dbo.Users");
            DropIndex("dbo.UserMobileDevices", new[] { "UserID" });
            RenameColumn(table: "dbo.UserMobileDevices", name: "UserID", newName: "User_Id");
            AlterColumn("dbo.UserMobileDevices", "User_Id", c => c.Int());
            CreateIndex("dbo.UserMobileDevices", "User_Id");
            AddForeignKey("dbo.UserMobileDevices", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserMobileDevices", "User_Id", "dbo.Users");
            DropIndex("dbo.UserMobileDevices", new[] { "User_Id" });
            AlterColumn("dbo.UserMobileDevices", "User_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.UserMobileDevices", name: "User_Id", newName: "UserID");
            CreateIndex("dbo.UserMobileDevices", "UserID");
            AddForeignKey("dbo.UserMobileDevices", "UserID", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
