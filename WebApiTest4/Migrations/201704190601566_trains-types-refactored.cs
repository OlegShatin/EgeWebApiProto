namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trainstypesrefactored : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Train", "Type_Id", "dbo.TrainType");
            DropIndex("dbo.Train", new[] { "Type_Id" });
            RenameColumn(table: "dbo.Train", name: "User_Id", newName: "User_Id2");
            RenameIndex(table: "dbo.Train", name: "IX_User_Id", newName: "IX_User_Id2");
            CreateTable(
                "dbo.EgeTrain",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        User_Id = c.Int(),
                        Points = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Train", t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.FreeTrain",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Train", t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.UserTaskAttempt", "Train_Id1", c => c.Int());
            CreateIndex("dbo.UserTaskAttempt", "Train_Id1");
            AddForeignKey("dbo.UserTaskAttempt", "Train_Id1", "dbo.Train", "Id");
            DropColumn("dbo.Train", "Type_Id");
            DropTable("dbo.TrainType");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TrainType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Train", "Type_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.FreeTrain", "User_Id", "dbo.User");
            DropForeignKey("dbo.FreeTrain", "Id", "dbo.Train");
            DropForeignKey("dbo.EgeTrain", "User_Id", "dbo.User");
            DropForeignKey("dbo.EgeTrain", "Id", "dbo.Train");
            DropForeignKey("dbo.UserTaskAttempt", "Train_Id1", "dbo.Train");
            DropIndex("dbo.FreeTrain", new[] { "User_Id" });
            DropIndex("dbo.FreeTrain", new[] { "Id" });
            DropIndex("dbo.EgeTrain", new[] { "User_Id" });
            DropIndex("dbo.EgeTrain", new[] { "Id" });
            DropIndex("dbo.UserTaskAttempt", new[] { "Train_Id1" });
            DropColumn("dbo.UserTaskAttempt", "Train_Id1");
            DropTable("dbo.FreeTrain");
            DropTable("dbo.EgeTrain");
            RenameIndex(table: "dbo.Train", name: "IX_User_Id2", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Train", name: "User_Id2", newName: "User_Id");
            CreateIndex("dbo.Train", "Type_Id");
            AddForeignKey("dbo.Train", "Type_Id", "dbo.TrainType", "Id", cascadeDelete: true);
        }
    }
}
