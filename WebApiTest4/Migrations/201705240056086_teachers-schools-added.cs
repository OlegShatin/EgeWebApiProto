namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teachersschoolsadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Schools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "TeacherId", c => c.Int());
            AddColumn("dbo.User", "School_Id", c => c.Int());
            CreateIndex("dbo.User", "TeacherId");
            CreateIndex("dbo.User", "School_Id");
            AddForeignKey("dbo.User", "School_Id", "dbo.Schools", "Id");
            AddForeignKey("dbo.User", "TeacherId", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "TeacherId", "dbo.User");
            DropForeignKey("dbo.User", "School_Id", "dbo.Schools");
            DropIndex("dbo.User", new[] { "School_Id" });
            DropIndex("dbo.User", new[] { "TeacherId" });
            DropColumn("dbo.User", "School_Id");
            DropColumn("dbo.User", "TeacherId");
            DropTable("dbo.Schools");
        }
    }
}
