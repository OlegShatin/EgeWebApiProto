namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimemigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "CreatedAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
