namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removetrains : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserTaskAttempt", "Train_Id", "dbo.Train");
            DropForeignKey("dbo.UserTaskAttempt", "Train_Id1", "dbo.Train");
            DropIndex("dbo.UserTaskAttempt", new[] { "Train_Id" });
            
            DropColumn("dbo.UserTaskAttempt", "Train_Id");

            DropForeignKey("dbo.Train", "User_Id", "dbo.User");
            
            DropForeignKey("dbo.EgeTrain", "Id", "dbo.Train");
            DropForeignKey("dbo.EgeTrain", "User_Id", "dbo.User");
            DropForeignKey("dbo.FreeTrain", "Id", "dbo.Train");
            DropForeignKey("dbo.FreeTrain", "User_Id", "dbo.User");
            DropIndex("dbo.Train", new[] { "User_Id" });
           
            DropIndex("dbo.EgeTrain", new[] { "Id" });
            DropIndex("dbo.EgeTrain", new[] { "User_Id" });
            DropIndex("dbo.FreeTrain", new[] { "Id" });
            DropIndex("dbo.FreeTrain", new[] { "User_Id" });
            
            DropTable("dbo.Train");
            DropTable("dbo.EgeTrain");
            DropTable("dbo.FreeTrain");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FreeTrain",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EgeTrain",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        User_Id = c.Int(),
                        Points = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Train",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(),
                        FinishTime = c.DateTime(),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserTaskAttempt", "Train_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.FreeTrain", "User_Id");
            CreateIndex("dbo.FreeTrain", "Id");
            CreateIndex("dbo.EgeTrain", "User_Id");
            CreateIndex("dbo.EgeTrain", "Id");
            CreateIndex("dbo.UserTaskAttempt", "Train_Id");
            CreateIndex("dbo.Train", "User_Id");
            AddForeignKey("dbo.FreeTrain", "User_Id", "dbo.User", "Id");
            AddForeignKey("dbo.FreeTrain", "Id", "dbo.Train", "Id");
            AddForeignKey("dbo.EgeTrain", "User_Id", "dbo.User", "Id");
            AddForeignKey("dbo.EgeTrain", "Id", "dbo.Train", "Id");
            AddForeignKey("dbo.UserTaskAttempt", "Train_Id", "dbo.Train", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Train", "User_Id", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
