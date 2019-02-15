namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewsFeedItemsTableWithRelationshipToUsersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsFeedItems",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        Title = c.String(maxLength: 100),
                        Body = c.String(nullable: false, maxLength: 500),
                        Img = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            //AddForeignKey("dbo.UserSubscriptions", "User_Feed_ID", "dbo.Users", "ID", cascadeDelete: false);
            //AddForeignKey("dbo.UserSubscriptions", "User_Sub_ID", "dbo.Users", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsFeedItems", "UserID", "dbo.Users");
            //DropForeignKey("dbo.UserSubscriptions", "User_Sub_ID", "dbo.Users");
            //DropForeignKey("dbo.UserSubscriptions", "User_Feed_ID", "dbo.Users");
            DropIndex("dbo.NewsFeedItems", new[] { "UserID" });
            DropTable("dbo.NewsFeedItems");
        }
    }
}
