namespace QuizProjectMvc.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueCategory : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.QuizCategories", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.QuizCategories", new[] { "Name" });
        }
    }
}
