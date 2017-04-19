namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bugfixed3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserTaskAttempt", "Train_Id", "dbo.Train");
            DropIndex("dbo.UserTaskAttempt", new[] { "Train_Id" });
            RenameColumn(table: "dbo.Train", name: "User_Id2", newName: "User_Id");
            RenameIndex(table: "dbo.Train", name: "IX_User_Id2", newName: "IX_User_Id");
            AlterColumn("dbo.UserTaskAttempt", "Train_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.UserTaskAttempt", "Train_Id");
            AddForeignKey("dbo.UserTaskAttempt", "Train_Id", "dbo.Train", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTaskAttempt", "Train_Id", "dbo.Train");
            DropIndex("dbo.UserTaskAttempt", new[] { "Train_Id" });
            AlterColumn("dbo.UserTaskAttempt", "Train_Id", c => c.Int());
            RenameIndex(table: "dbo.Train", name: "IX_User_Id", newName: "IX_User_Id2");
            RenameColumn(table: "dbo.Train", name: "User_Id", newName: "User_Id2");
            CreateIndex("dbo.UserTaskAttempt", "Train_Id");
            AddForeignKey("dbo.UserTaskAttempt", "Train_Id", "dbo.Train", "Id");
        }
    }
}
