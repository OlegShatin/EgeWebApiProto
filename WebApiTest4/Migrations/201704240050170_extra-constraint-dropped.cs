namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extraconstraintdropped : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trains", "StartTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trains", "StartTime", c => c.DateTime());
        }
    }
}
