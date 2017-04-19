namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bugfixed : DbMigration
    {
        public override void Up()
        {
            
            //DropForeignKey("dbo.UserTaskAttempt","Train_Id");
            //Sql("ALTER TABLE [dbo].[UserTaskAttempt] DROP CONSTRAINT [FK_dbo.UserTaskAttempt_dbo.User_Train_Id]");
            DropForeignKey("dbo.UserTaskAttempt", "Train_Id", "dbo.Train");
            DropIndex("dbo.UserTaskAttempt", new[] { "Train_Id" });
            DropIndex("dbo.UserTaskAttempt", new[] { "Train_Id1" });
            DropColumn("dbo.UserTaskAttempt", "Train_Id");
            RenameColumn(table: "dbo.UserTaskAttempt", name: "Train_Id1", newName: "Train_Id");
            AlterColumn("dbo.UserTaskAttempt", "Train_Id", c => c.Int());
            CreateIndex("dbo.UserTaskAttempt", "Train_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserTaskAttempt", new[] { "Train_Id" });
            AlterColumn("dbo.UserTaskAttempt", "Train_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.UserTaskAttempt", name: "Train_Id", newName: "Train_Id1");
            AddColumn("dbo.UserTaskAttempt", "Train_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.UserTaskAttempt", "Train_Id1");
            CreateIndex("dbo.UserTaskAttempt", "Train_Id");
            AddForeignKey("dbo.UserTaskAttempt", "Train_Id", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
