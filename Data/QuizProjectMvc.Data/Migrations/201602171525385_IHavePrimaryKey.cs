namespace QuizProjectMvc.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class IHavePrimaryKey : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.AspNetUsers", "CreatedOn", c => c.DateTime(false));
            this.AddColumn("dbo.AspNetUsers", "ModifiedOn", c => c.DateTime());
            this.AddColumn("dbo.AspNetUsers", "IsDeleted", c => c.Boolean(false));
            this.AddColumn("dbo.AspNetUsers", "DeletedOn", c => c.DateTime());
            this.DropColumn("dbo.AspNetUsers", "RegisteredOn");
        }

        public override void Down()
        {
            this.AddColumn("dbo.AspNetUsers", "RegisteredOn", c => c.DateTime(false));
            this.DropColumn("dbo.AspNetUsers", "DeletedOn");
            this.DropColumn("dbo.AspNetUsers", "IsDeleted");
            this.DropColumn("dbo.AspNetUsers", "ModifiedOn");
            this.DropColumn("dbo.AspNetUsers", "CreatedOn");
        }
    }
}