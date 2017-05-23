namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attempttosavelinkslist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTaskAttempts", "Comment", c => c.String());
            AddColumn("dbo.UserTaskAttempts", "LinksAsString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTaskAttempts", "LinksAsString");
            DropColumn("dbo.UserTaskAttempts", "Comment");
        }
    }
}
