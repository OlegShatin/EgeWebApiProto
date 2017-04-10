namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedisshortfieldtotopics : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskTopic", "IsShort", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskTopic", "IsShort");
        }
    }
}
