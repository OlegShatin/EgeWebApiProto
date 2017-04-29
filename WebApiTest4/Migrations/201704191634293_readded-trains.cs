namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class readdedtrains : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.FreeTrains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(),
                        FinishTime = c.DateTime(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.UserTaskAttempt", "EgeTrain_Id", c => c.Int());
            AddColumn("dbo.UserTaskAttempt", "FreeTrain_Id", c => c.Int());
            CreateIndex("dbo.UserTaskAttempt", "EgeTrain_Id");
            CreateIndex("dbo.UserTaskAttempt", "FreeTrain_Id");
            AddForeignKey("dbo.UserTaskAttempt", "EgeTrain_Id", "dbo.ExamTrains", "Id");
            AddForeignKey("dbo.UserTaskAttempt", "FreeTrain_Id", "dbo.FreeTrains", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FreeTrains", "User_Id", "dbo.User");
            DropForeignKey("dbo.UserTaskAttempt", "FreeTrain_Id", "dbo.FreeTrains");
            DropForeignKey("dbo.ExamTrains", "User_Id", "dbo.User");
            DropForeignKey("dbo.UserTaskAttempt", "EgeTrain_Id", "dbo.ExamTrains");
            DropIndex("dbo.FreeTrains", new[] { "User_Id" });
            DropIndex("dbo.UserTaskAttempt", new[] { "FreeTrain_Id" });
            DropIndex("dbo.UserTaskAttempt", new[] { "EgeTrain_Id" });
            DropIndex("dbo.ExamTrains", new[] { "User_Id" });
            DropColumn("dbo.UserTaskAttempt", "FreeTrain_Id");
            DropColumn("dbo.UserTaskAttempt", "EgeTrain_Id");
            DropTable("dbo.FreeTrains");
            DropTable("dbo.ExamTrains");
        }
    }
}
