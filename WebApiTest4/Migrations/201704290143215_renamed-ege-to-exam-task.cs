namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamedegetoexamtask : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EgeTask", newName: "ExamTasks");
            RenameColumn(table: "dbo.UserTaskAttempt", name: "EgeTask_Id", newName: "ExamTask_Id");
            RenameIndex(table: "dbo.UserTaskAttempt", name: "IX_EgeTask_Id", newName: "IX_ExamTask_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.UserTaskAttempt", name: "IX_ExamTask_Id", newName: "IX_EgeTask_Id");
            RenameColumn(table: "dbo.UserTaskAttempt", name: "ExamTask_Id", newName: "EgeTask_Id");
            RenameTable(name: "dbo.ExamTasks", newName: "EgeTask");
        }
    }
}
