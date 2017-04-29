namespace WebApiTest4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedexamtype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.User", "CurrentExam_Id", c => c.Int());
            CreateIndex("dbo.User", "CurrentExam_Id");
            AddForeignKey("dbo.User", "CurrentExam_Id", "dbo.Exams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "CurrentExam_Id", "dbo.Exams");
            DropIndex("dbo.User", new[] { "CurrentExam_Id" });
            DropColumn("dbo.User", "CurrentExam_Id");
            DropTable("dbo.Exams");
        }
    }
}
