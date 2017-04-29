namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedexamtypefk7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trains", "Exam_Id", "dbo.Exams");
            DropForeignKey("dbo.TaskTopics", "Exam_Id", "dbo.Exams");
            DropIndex("dbo.Trains", new[] { "Exam_Id" });
            DropIndex("dbo.TaskTopics", new[] { "Exam_Id" });
            AlterColumn("dbo.Trains", "Exam_Id", c => c.Int());
            AlterColumn("dbo.TaskTopics", "Exam_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Trains", "Exam_Id");
            CreateIndex("dbo.TaskTopics", "Exam_Id");
            AddForeignKey("dbo.Trains", "Exam_Id", "dbo.Exams", "Id");
            AddForeignKey("dbo.TaskTopics", "Exam_Id", "dbo.Exams", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskTopics", "Exam_Id", "dbo.Exams");
            DropForeignKey("dbo.Trains", "Exam_Id", "dbo.Exams");
            DropIndex("dbo.TaskTopics", new[] { "Exam_Id" });
            DropIndex("dbo.Trains", new[] { "Exam_Id" });
            AlterColumn("dbo.TaskTopics", "Exam_Id", c => c.Int());
            AlterColumn("dbo.Trains", "Exam_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.TaskTopics", "Exam_Id");
            CreateIndex("dbo.Trains", "Exam_Id");
            AddForeignKey("dbo.TaskTopics", "Exam_Id", "dbo.Exams", "Id");
            AddForeignKey("dbo.Trains", "Exam_Id", "dbo.Exams", "Id", cascadeDelete: true);
        }
    }
}
