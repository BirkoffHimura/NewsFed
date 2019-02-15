namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueConstraintOnUserSubscriptionTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserSubscriptions", new[] { "User_Sub_ID" });
            DropIndex("dbo.UserSubscriptions", new[] { "User_Feed_ID" });
            CreateIndex("dbo.UserSubscriptions", new[] { "User_Sub_ID", "User_Feed_ID" }, unique: true, name: "IX_SubFollow");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserSubscriptions", "IX_SubFollow");
            CreateIndex("dbo.UserSubscriptions", "User_Feed_ID");
            CreateIndex("dbo.UserSubscriptions", "User_Sub_ID");
        }
    }
}
