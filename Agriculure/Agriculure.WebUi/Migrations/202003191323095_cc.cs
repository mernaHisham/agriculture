namespace Agriculure.WebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "CreationDate");
        }
    }
}
