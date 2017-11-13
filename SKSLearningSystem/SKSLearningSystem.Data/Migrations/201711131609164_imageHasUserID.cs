namespace SKSLearningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageHasUserID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "UserId");
        }
    }
}
