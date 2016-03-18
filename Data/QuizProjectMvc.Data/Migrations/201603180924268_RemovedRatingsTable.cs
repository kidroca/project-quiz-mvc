namespace QuizProjectMvc.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRatingsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuizRatings", "ByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.QuizRatings", "QuizId", "dbo.Quizs");
            DropIndex("dbo.QuizRatings", new[] { "QuizId" });
            DropIndex("dbo.QuizRatings", new[] { "ByUserId" });
            DropTable("dbo.QuizRatings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.QuizRatings",
                c => new
                    {
                        QuizId = c.Int(nullable: false),
                        ByUserId = c.String(nullable: false, maxLength: 128),
                        Value = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuizId, t.ByUserId });
            
            CreateIndex("dbo.QuizRatings", "ByUserId");
            CreateIndex("dbo.QuizRatings", "QuizId");
            AddForeignKey("dbo.QuizRatings", "QuizId", "dbo.Quizs", "Id");
            AddForeignKey("dbo.QuizRatings", "ByUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
