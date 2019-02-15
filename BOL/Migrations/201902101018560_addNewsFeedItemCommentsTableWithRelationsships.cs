namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewsFeedItemCommentsTableWithRelationsships : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsFeedItemComments",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Comment_Body = c.String(),
                        CommentDate = c.DateTime(nullable: false),
                        CommentUserID = c.Long(nullable: false),
                        NewsFeedItemID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.NewsFeedItems", t => t.NewsFeedItemID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CommentUserID, cascadeDelete: false)
                .Index(t => t.CommentUserID)
                .Index(t => t.NewsFeedItemID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsFeedItemComments", "CommentUserID", "dbo.Users");
            DropForeignKey("dbo.NewsFeedItemComments", "NewsFeedItemID", "dbo.NewsFeedItems");
            DropIndex("dbo.NewsFeedItemComments", new[] { "NewsFeedItemID" });
            DropIndex("dbo.NewsFeedItemComments", new[] { "CommentUserID" });
            DropTable("dbo.NewsFeedItemComments");
        }
    }
}
