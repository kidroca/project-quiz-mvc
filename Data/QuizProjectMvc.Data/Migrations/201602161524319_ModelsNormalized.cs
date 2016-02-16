namespace QuizProjectMvc.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelsNormalized : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Answers", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Questions", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Questions", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Questions", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Questions", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Quizs", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Quizs", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Quizs", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.QuizCategories", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.QuizCategories", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.QuizCategories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.QuizCategories", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.QuizSolutions", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.QuizSolutions", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.QuizSolutions", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.QuizSolutions", "DeletedOn", c => c.DateTime());
            CreateIndex("dbo.Questions", "IsDeleted");
            CreateIndex("dbo.Quizs", "IsDeleted");
            CreateIndex("dbo.QuizCategories", "IsDeleted");
            CreateIndex("dbo.QuizSolutions", "IsDeleted");
        }
        
        public override void Down()
        {
            DropIndex("dbo.QuizSolutions", new[] { "IsDeleted" });
            DropIndex("dbo.QuizCategories", new[] { "IsDeleted" });
            DropIndex("dbo.Quizs", new[] { "IsDeleted" });
            DropIndex("dbo.Questions", new[] { "IsDeleted" });
            DropColumn("dbo.QuizSolutions", "DeletedOn");
            DropColumn("dbo.QuizSolutions", "IsDeleted");
            DropColumn("dbo.QuizSolutions", "ModifiedOn");
            DropColumn("dbo.QuizSolutions", "CreatedOn");
            DropColumn("dbo.QuizCategories", "DeletedOn");
            DropColumn("dbo.QuizCategories", "IsDeleted");
            DropColumn("dbo.QuizCategories", "ModifiedOn");
            DropColumn("dbo.QuizCategories", "CreatedOn");
            DropColumn("dbo.Quizs", "DeletedOn");
            DropColumn("dbo.Quizs", "IsDeleted");
            DropColumn("dbo.Quizs", "ModifiedOn");
            DropColumn("dbo.Questions", "DeletedOn");
            DropColumn("dbo.Questions", "IsDeleted");
            DropColumn("dbo.Questions", "ModifiedOn");
            DropColumn("dbo.Questions", "CreatedOn");
            DropColumn("dbo.Answers", "DeletedOn");
            DropColumn("dbo.Answers", "IsDeleted");
        }
    }
}
