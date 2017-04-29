namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class readdedtrains1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserTaskAttempt", "EgeTrain_Id", "dbo.ExamTrains");
            DropForeignKey("dbo.ExamTrains", "User_Id", "dbo.User");
            DropForeignKey("dbo.UserTaskAttempt", "FreeTrain_Id", "dbo.FreeTrains");
            DropForeignKey("dbo.FreeTrains", "User_Id", "dbo.User");
            DropIndex("dbo.ExamTrains", new[] { "User_Id" });
            DropIndex("dbo.UserTaskAttempt", new[] { "EgeTrain_Id" });
            DropIndex("dbo.UserTaskAttempt", new[] { "FreeTrain_Id" });
            DropIndex("dbo.FreeTrains", new[] { "User_Id" });
            DropColumn("dbo.UserTaskAttempt", "EgeTrain_Id");
            DropColumn("dbo.UserTaskAttempt", "FreeTrain_Id");
            DropTable("dbo.ExamTrains");
            DropTable("dbo.FreeTrains");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FreeTrains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(),
                        FinishTime = c.DateTime(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExamTrains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Points = c.Int(),
                        StartTime = c.DateTime(),
                        FinishTime = c.DateTime(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserTaskAttempt", "FreeTrain_Id", c => c.Int());
            AddColumn("dbo.UserTaskAttempt", "EgeTrain_Id", c => c.Int());
            CreateIndex("dbo.FreeTrains", "User_Id");
            CreateIndex("dbo.UserTaskAttempt", "FreeTrain_Id");
            CreateIndex("dbo.UserTaskAttempt", "EgeTrain_Id");
            CreateIndex("dbo.ExamTrains", "User_Id");
            AddForeignKey("dbo.FreeTrains", "User_Id", "dbo.User", "Id");
            AddForeignKey("dbo.UserTaskAttempt", "FreeTrain_Id", "dbo.FreeTrains", "Id");
            AddForeignKey("dbo.ExamTrains", "User_Id", "dbo.User", "Id");
            AddForeignKey("dbo.UserTaskAttempt", "EgeTrain_Id", "dbo.ExamTrains", "Id");
        }
    }
}
