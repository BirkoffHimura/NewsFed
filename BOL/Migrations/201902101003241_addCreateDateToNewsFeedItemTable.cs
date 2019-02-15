namespace BOL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreateDateToNewsFeedItemTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsFeedItems", "CreateDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewsFeedItems", "CreateDate");
        }
    }
}
