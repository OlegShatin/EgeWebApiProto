namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class topiccodes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaskTopics", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaskTopics", "Code");
        }
    }
}
