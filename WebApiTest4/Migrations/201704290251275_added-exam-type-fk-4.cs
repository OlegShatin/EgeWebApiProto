namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedexamtypefk4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trains", "Exam_Id", "dbo.Exams");
            DropIndex("dbo.Trains", new[] { "Exam_Id" });
            AlterColumn("dbo.Trains", "Exam_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Trains", "Exam_Id");
            AddForeignKey("dbo.Trains", "Exam_Id", "dbo.Exams", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trains", "Exam_Id", "dbo.Exams");
            DropIndex("dbo.Trains", new[] { "Exam_Id" });
            AlterColumn("dbo.Trains", "Exam_Id", c => c.Int());
            CreateIndex("dbo.Trains", "Exam_Id");
            AddForeignKey("dbo.Trains", "Exam_Id", "dbo.Exams", "Id");
        }
    }
}
