namespace QuizProjectMvc.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizQuestions1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "ShuffleAnswers", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "ShuffleAnswers");
        }
    }
}
