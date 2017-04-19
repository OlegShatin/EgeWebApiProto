namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class readdedtrains3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserTaskAttempt", "Train_Id", "dbo.Trains");
            DropIndex("dbo.UserTaskAttempt", new[] { "Train_Id" });
            AlterColumn("dbo.UserTaskAttempt", "Train_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.UserTaskAttempt", "Train_Id");
            AddForeignKey("dbo.UserTaskAttempt", "Train_Id", "dbo.Trains", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTaskAttempt", "Train_Id", "dbo.Trains");
            DropIndex("dbo.UserTaskAttempt", new[] { "Train_Id" });
            AlterColumn("dbo.UserTaskAttempt", "Train_Id", c => c.Int());
            CreateIndex("dbo.UserTaskAttempt", "Train_Id");
            AddForeignKey("dbo.UserTaskAttempt", "Train_Id", "dbo.Trains", "Id");
        }
    }
}
