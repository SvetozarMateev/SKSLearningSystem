namespace SKSLearningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class questionsmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Questions", "CourseId");
            AddForeignKey("dbo.Questions", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "CourseId", "dbo.Courses");
            DropIndex("dbo.Questions", new[] { "CourseId" });
            DropColumn("dbo.Questions", "CourseId");
        }
    }
}
