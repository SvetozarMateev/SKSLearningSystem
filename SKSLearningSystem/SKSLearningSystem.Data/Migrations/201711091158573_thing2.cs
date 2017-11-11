namespace SKSLearningSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thing2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "Statement", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Questions", "Answer", c => c.String(nullable: false, maxLength: 2));
            AlterColumn("dbo.Options", "Letter", c => c.String(nullable: false, maxLength: 2));
            AlterColumn("dbo.Options", "Answer", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Options", "Answer", c => c.String());
            AlterColumn("dbo.Options", "Letter", c => c.String());
            AlterColumn("dbo.Questions", "Answer", c => c.String());
            AlterColumn("dbo.Questions", "Statement", c => c.String());
        }
    }
}
