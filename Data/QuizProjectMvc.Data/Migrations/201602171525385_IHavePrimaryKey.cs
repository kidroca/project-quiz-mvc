namespace QuizProjectMvc.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IHavePrimaryKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "DeletedOn", c => c.DateTime());
            DropColumn("dbo.AspNetUsers", "RegisteredOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "RegisteredOn", c => c.DateTime(nullable: false));
            DropColumn("dbo.AspNetUsers", "DeletedOn");
            DropColumn("dbo.AspNetUsers", "IsDeleted");
            DropColumn("dbo.AspNetUsers", "ModifiedOn");
            DropColumn("dbo.AspNetUsers", "CreatedOn");
        }
    }
}
