namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attemptsdivided : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserTaskAttempts", "CheckTime", c => c.DateTime());
            AddColumn("dbo.UserTaskAttempts", "IsChecked", c => c.Boolean());
            AddColumn("dbo.UserTaskAttempts", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserTaskAttempts", "Discriminator");
            DropColumn("dbo.UserTaskAttempts", "IsChecked");
            DropColumn("dbo.UserTaskAttempts", "CheckTime");
        }
    }
}
