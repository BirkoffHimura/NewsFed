namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUsersTableIncludeAdminAcctAndAllowPostColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AdminAcct", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "AllowPost", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "AllowPost");
            DropColumn("dbo.Users", "AdminAcct");
        }
    }
}
