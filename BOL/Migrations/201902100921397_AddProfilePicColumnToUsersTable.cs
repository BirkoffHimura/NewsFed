namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfilePicColumnToUsersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ProfilePic", c => c.String(nullable: false, maxLength: 60));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ProfilePic");
        }
    }
}
