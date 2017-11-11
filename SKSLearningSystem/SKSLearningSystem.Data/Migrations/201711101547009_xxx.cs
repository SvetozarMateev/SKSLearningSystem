namespace SKSLearningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xxx : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CourseStates", new[] { "User_Id" });
            DropColumn("dbo.CourseStates", "UserId");
            RenameColumn(table: "dbo.CourseStates", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.CourseStates", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.CourseStates", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CourseStates", new[] { "UserId" });
            AlterColumn("dbo.CourseStates", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.CourseStates", name: "UserId", newName: "User_Id");
            AddColumn("dbo.CourseStates", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.CourseStates", "User_Id");
        }
    }
}
