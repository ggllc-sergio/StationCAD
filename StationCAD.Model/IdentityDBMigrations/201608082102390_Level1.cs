namespace StationCAD.Model.IdentityDBMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Level1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrganizationUserAffiliations", "CurrentUser_Id", "dbo.User");
            DropIndex("dbo.OrganizationUserAffiliations", new[] { "CurrentUser_Id" });
            RenameColumn(table: "dbo.UserMobileDevices", name: "User_Id", newName: "UserProfile_Id");
            RenameColumn(table: "dbo.OrganizationUserAffiliations", name: "UserProfile_Id", newName: "CurrentUserProfile_Id");
            RenameIndex(table: "dbo.UserMobileDevices", name: "IX_User_Id", newName: "IX_UserProfile_Id");
            RenameIndex(table: "dbo.OrganizationUserAffiliations", name: "IX_UserProfile_Id", newName: "IX_CurrentUserProfile_Id");
            DropColumn("dbo.OrganizationUserAffiliations", "CurrentUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrganizationUserAffiliations", "CurrentUser_Id", c => c.String(maxLength: 128));
            RenameIndex(table: "dbo.OrganizationUserAffiliations", name: "IX_CurrentUserProfile_Id", newName: "IX_UserProfile_Id");
            RenameIndex(table: "dbo.UserMobileDevices", name: "IX_UserProfile_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.OrganizationUserAffiliations", name: "CurrentUserProfile_Id", newName: "UserProfile_Id");
            RenameColumn(table: "dbo.UserMobileDevices", name: "UserProfile_Id", newName: "User_Id");
            CreateIndex("dbo.OrganizationUserAffiliations", "CurrentUser_Id");
            AddForeignKey("dbo.OrganizationUserAffiliations", "CurrentUser_Id", "dbo.User", "UserId");
        }
    }
}
