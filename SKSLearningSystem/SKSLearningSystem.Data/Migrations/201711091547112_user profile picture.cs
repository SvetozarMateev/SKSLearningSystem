namespace SKSLearningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userprofilepicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ProfilePicture_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "ProfilePicture_Id");
            AddForeignKey("dbo.AspNetUsers", "ProfilePicture_Id", "dbo.Images", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ProfilePicture_Id", "dbo.Images");
            DropIndex("dbo.AspNetUsers", new[] { "ProfilePicture_Id" });
            DropColumn("dbo.AspNetUsers", "ProfilePicture_Id");
        }
    }
}
