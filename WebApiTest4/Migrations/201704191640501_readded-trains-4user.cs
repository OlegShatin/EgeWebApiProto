namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class readdedtrains4user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trains", "User_Id1", c => c.Int());
            AddColumn("dbo.Trains", "User_Id2", c => c.Int());
            CreateIndex("dbo.Trains", "User_Id1");
            CreateIndex("dbo.Trains", "User_Id2");
            AddForeignKey("dbo.Trains", "User_Id1", "dbo.User", "Id");
            AddForeignKey("dbo.Trains", "User_Id2", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trains", "User_Id2", "dbo.User");
            DropForeignKey("dbo.Trains", "User_Id1", "dbo.User");
            DropIndex("dbo.Trains", new[] { "User_Id2" });
            DropIndex("dbo.Trains", new[] { "User_Id1" });
            DropColumn("dbo.Trains", "User_Id2");
            DropColumn("dbo.Trains", "User_Id1");
        }
    }
}
