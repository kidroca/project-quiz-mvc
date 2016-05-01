namespace QuizProjectMvc.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizQuestions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "NumberOfQuestions", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "NumberOfQuestions");
        }
    }
}
