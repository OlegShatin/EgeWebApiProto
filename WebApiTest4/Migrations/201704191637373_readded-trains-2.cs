namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class readdedtrains2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(),
                        FinishTime = c.DateTime(),
                        Points = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.UserTaskAttempt", "Train_Id", c => c.Int());
            CreateIndex("dbo.UserTaskAttempt", "Train_Id");
            AddForeignKey("dbo.UserTaskAttempt", "Train_Id", "dbo.Trains", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trains", "User_Id", "dbo.User");
            DropForeignKey("dbo.UserTaskAttempt", "Train_Id", "dbo.Trains");
            DropIndex("dbo.UserTaskAttempt", new[] { "Train_Id" });
            DropIndex("dbo.Trains", new[] { "User_Id" });
            DropColumn("dbo.UserTaskAttempt", "Train_Id");
            DropTable("dbo.Trains");
        }
    }
}
