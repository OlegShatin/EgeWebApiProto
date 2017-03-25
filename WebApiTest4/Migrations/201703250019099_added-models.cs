namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EgeTask",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Answer = c.String(nullable: false),
                        Topic_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskTopic", t => t.Topic_Id, cascadeDelete: true)
                .Index(t => t.Topic_Id);
            
            CreateTable(
                "dbo.TaskTopic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Number = c.Int(nullable: false),
                        PointsPerTask = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Number, unique: true);
            
            CreateTable(
                "dbo.UserTaskAttempt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserAnswer = c.String(),
                        Points = c.Int(nullable: false),
                        EgeTask_Id = c.Int(nullable: false),
                        Train_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EgeTask", t => t.EgeTask_Id, cascadeDelete: true)
                .ForeignKey("dbo.Train", t => t.Train_Id, cascadeDelete: true)
                .Index(t => t.EgeTask_Id)
                .Index(t => t.Train_Id);
            
            CreateTable(
                "dbo.Train",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(),
                        FinishTime = c.DateTime(),
                        Type_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrainType", t => t.Type_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Type_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.TrainType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.User", "Points", c => c.Int(nullable: false));
            AddColumn("dbo.User", "UsePoints", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTaskAttempt", "Train_Id", "dbo.Train");
            DropForeignKey("dbo.Train", "User_Id", "dbo.User");
            DropForeignKey("dbo.Train", "Type_Id", "dbo.TrainType");
            DropForeignKey("dbo.UserTaskAttempt", "EgeTask_Id", "dbo.EgeTask");
            DropForeignKey("dbo.EgeTask", "Topic_Id", "dbo.TaskTopic");
            DropIndex("dbo.Train", new[] { "User_Id" });
            DropIndex("dbo.Train", new[] { "Type_Id" });
            DropIndex("dbo.UserTaskAttempt", new[] { "Train_Id" });
            DropIndex("dbo.UserTaskAttempt", new[] { "EgeTask_Id" });
            DropIndex("dbo.TaskTopic", new[] { "Number" });
            DropIndex("dbo.EgeTask", new[] { "Topic_Id" });
            DropColumn("dbo.User", "UsePoints");
            DropColumn("dbo.User", "Points");
            DropColumn("dbo.User", "CreatedAt");
            DropTable("dbo.TrainType");
            DropTable("dbo.Train");
            DropTable("dbo.UserTaskAttempt");
            DropTable("dbo.TaskTopic");
            DropTable("dbo.EgeTask");
        }
    }
}
