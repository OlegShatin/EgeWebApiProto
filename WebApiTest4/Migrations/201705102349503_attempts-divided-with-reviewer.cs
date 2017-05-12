namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attemptsdividedwithreviewer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTaskAttempts", "Reviewer_Id", c => c.Int());
            CreateIndex("dbo.UserTaskAttempts", "Reviewer_Id");
            AddForeignKey("dbo.UserTaskAttempts", "Reviewer_Id", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTaskAttempts", "Reviewer_Id", "dbo.User");
            DropIndex("dbo.UserTaskAttempts", new[] { "Reviewer_Id" });
            DropColumn("dbo.UserTaskAttempts", "Reviewer_Id");
        }
    }
}
