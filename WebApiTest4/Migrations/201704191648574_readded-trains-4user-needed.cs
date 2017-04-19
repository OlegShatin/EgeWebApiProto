namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class readdedtrains4userneeded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trains", "User_Id", "dbo.User");
            DropIndex("dbo.Trains", new[] { "User_Id" });
            AlterColumn("dbo.Trains", "User_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Trains", "User_Id");
            AddForeignKey("dbo.Trains", "User_Id", "dbo.User", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trains", "User_Id", "dbo.User");
            DropIndex("dbo.Trains", new[] { "User_Id" });
            AlterColumn("dbo.Trains", "User_Id", c => c.Int());
            CreateIndex("dbo.Trains", "User_Id");
            AddForeignKey("dbo.Trains", "User_Id", "dbo.User", "Id");
        }
    }
}
