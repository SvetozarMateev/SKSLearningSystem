namespace SKSLearningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userImageImageID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "UserId", c => c.String());
            AddColumn("dbo.AspNetUsers", "ProfilePic_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "ProfilePic_Id");
            AddForeignKey("dbo.AspNetUsers", "ProfilePic_Id", "dbo.Images", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ProfilePic_Id", "dbo.Images");
            DropIndex("dbo.AspNetUsers", new[] { "ProfilePic_Id" });
            DropColumn("dbo.AspNetUsers", "ProfilePic_Id");
            DropColumn("dbo.Images", "UserId");
        }
    }
}
