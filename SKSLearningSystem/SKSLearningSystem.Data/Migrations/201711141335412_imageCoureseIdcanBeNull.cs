namespace SKSLearningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageCoureseIdcanBeNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "CourseId", "dbo.Courses");
            DropIndex("dbo.Images", new[] { "CourseId" });
            AlterColumn("dbo.Images", "CourseId", c => c.Int());
            CreateIndex("dbo.Images", "CourseId");
            AddForeignKey("dbo.Images", "CourseId", "dbo.Courses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "CourseId", "dbo.Courses");
            DropIndex("dbo.Images", new[] { "CourseId" });
            AlterColumn("dbo.Images", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Images", "CourseId");
            AddForeignKey("dbo.Images", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
    }
}
