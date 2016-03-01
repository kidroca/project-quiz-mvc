namespace QuizProjectMvc.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionsCascadeDeleteAnswers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "ForQuestionId", "dbo.Questions");
            AddForeignKey("dbo.Answers", "ForQuestionId", "dbo.Questions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "ForQuestionId", "dbo.Questions");
            AddForeignKey("dbo.Answers", "ForQuestionId", "dbo.Questions", "Id");
        }
    }
}
