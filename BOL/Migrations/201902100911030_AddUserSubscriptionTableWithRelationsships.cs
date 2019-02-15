namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserSubscriptionTableWithRelationsships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSubscriptions",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        User_Sub_ID = c.Long(nullable: false),
                        User_Feed_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User_Feed_ID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.User_Sub_ID, cascadeDelete: false)
                .Index(t => t.User_Sub_ID)
                .Index(t => t.User_Feed_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSubscriptions", "User_Sub_ID", "dbo.Users");
            DropForeignKey("dbo.UserSubscriptions", "User_Feed_ID", "dbo.Users");
            DropIndex("dbo.UserSubscriptions", new[] { "User_Feed_ID" });
            DropIndex("dbo.UserSubscriptions", new[] { "User_Sub_ID" });
            DropTable("dbo.UserSubscriptions");
        }
    }
}
