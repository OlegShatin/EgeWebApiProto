namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednumtoegetask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EgeTask", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EgeTask", "Number");
        }
    }
}
