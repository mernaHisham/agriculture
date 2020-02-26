namespace Agriculure.WebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Offer", new[] { "offerowner" });
            AlterColumn("dbo.Offer", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Offer", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Offer", "Status", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Offer", "offerowner", c => c.Long(nullable: false));
            AlterColumn("dbo.Offer", "Descreption", c => c.String(nullable: false, maxLength: 4000));
            AlterColumn("dbo.Offer", "unit", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Offer", "quntity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Offer", "price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Offer", "currency", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Offer", "offerowner");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Offer", new[] { "offerowner" });
            AlterColumn("dbo.Offer", "currency", c => c.String(maxLength: 50));
            AlterColumn("dbo.Offer", "price", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Offer", "quntity", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Offer", "unit", c => c.String(maxLength: 50));
            AlterColumn("dbo.Offer", "Descreption", c => c.String(maxLength: 4000));
            AlterColumn("dbo.Offer", "offerowner", c => c.Long());
            AlterColumn("dbo.Offer", "Status", c => c.Boolean());
            AlterColumn("dbo.Offer", "EndDate", c => c.DateTime());
            AlterColumn("dbo.Offer", "StartDate", c => c.DateTime());
            CreateIndex("dbo.Offer", "offerowner");
        }
    }
}
