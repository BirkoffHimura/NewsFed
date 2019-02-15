namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaxLengthToComment_BodyColumnOnNewsFeedItemCommentsTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NewsFeedItemComments", "Comment_Body", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NewsFeedItemComments", "Comment_Body", c => c.String());
        }
    }
}
