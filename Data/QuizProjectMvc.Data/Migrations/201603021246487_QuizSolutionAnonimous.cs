namespace QuizProjectMvc.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizSolutionAnonimous : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.QuizSolutions", new[] { "ByUserId" });
            AlterColumn("dbo.QuizSolutions", "ByUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.QuizSolutions", "ByUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.QuizSolutions", new[] { "ByUserId" });
            AlterColumn("dbo.QuizSolutions", "ByUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.QuizSolutions", "ByUserId");
        }
    }
}
