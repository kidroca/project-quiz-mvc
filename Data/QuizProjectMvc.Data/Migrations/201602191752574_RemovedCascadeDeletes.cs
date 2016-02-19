namespace QuizProjectMvc.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedCascadeDeletes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "ForQuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.Quizs", "CategoryId", "dbo.QuizCategories");
            DropForeignKey("dbo.Quizs", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.QuizRatings", "ByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.QuizSolutions", "ByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.String(nullable: false, maxLength: 128),
                        Content = c.String(nullable: false, maxLength: 2000),
                        QuizId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Quizs", t => t.QuizId)
                .Index(t => t.AuthorId)
                .Index(t => t.QuizId)
                .Index(t => t.IsDeleted);
            
            AddForeignKey("dbo.Answers", "ForQuestionId", "dbo.Questions", "Id");
            AddForeignKey("dbo.Questions", "QuizId", "dbo.Quizs", "Id");
            AddForeignKey("dbo.Quizs", "CategoryId", "dbo.QuizCategories", "Id");
            AddForeignKey("dbo.Quizs", "CreatedById", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.QuizRatings", "ByUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.QuizSolutions", "ByUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.QuizSolutions", "ByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.QuizRatings", "ByUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Quizs", "CreatedById", "dbo.AspNetUsers");
            DropForeignKey("dbo.Quizs", "CategoryId", "dbo.QuizCategories");
            DropForeignKey("dbo.Questions", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.Answers", "ForQuestionId", "dbo.Questions");
            DropForeignKey("dbo.Comments", "QuizId", "dbo.Quizs");
            DropForeignKey("dbo.Comments", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "IsDeleted" });
            DropIndex("dbo.Comments", new[] { "QuizId" });
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            DropTable("dbo.Comments");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.QuizSolutions", "ByUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.QuizRatings", "ByUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Quizs", "CreatedById", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Quizs", "CategoryId", "dbo.QuizCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "QuizId", "dbo.Quizs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Answers", "ForQuestionId", "dbo.Questions", "Id", cascadeDelete: true);
        }
    }
}
