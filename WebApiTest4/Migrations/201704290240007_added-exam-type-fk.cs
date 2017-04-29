namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedexamtypefk : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TaskTopic", newName: "TaskTopics");
            AddColumn("dbo.Trains", "Exam_Id", c => c.Int());
            AddColumn("dbo.TaskTopics", "Exam_Id", c => c.Int());
            CreateIndex("dbo.Trains", "Exam_Id");
            CreateIndex("dbo.TaskTopics", "Exam_Id");
            AddForeignKey("dbo.Trains", "Exam_Id", "dbo.Exams", "Id");
            AddForeignKey("dbo.TaskTopics", "Exam_Id", "dbo.Exams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskTopics", "Exam_Id", "dbo.Exams");
            DropForeignKey("dbo.Trains", "Exam_Id", "dbo.Exams");
            DropIndex("dbo.TaskTopics", new[] { "Exam_Id" });
            DropIndex("dbo.Trains", new[] { "Exam_Id" });
            DropColumn("dbo.TaskTopics", "Exam_Id");
            DropColumn("dbo.Trains", "Exam_Id");
            RenameTable(name: "dbo.TaskTopics", newName: "TaskTopic");
        }
    }
}
