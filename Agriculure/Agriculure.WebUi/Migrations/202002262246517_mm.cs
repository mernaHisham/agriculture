namespace Agriculure.WebUi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        OfferID = c.Long(nullable: false),
                        Quantity = c.Int(),
                        Price = c.Double(),
                        buyerID = c.Long(),
                        sellerID = c.Long(),
                        requestDate = c.DateTime(),
                        status = c.Boolean(),
                        acceptDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Offer", t => t.OfferID)
                .ForeignKey("dbo.Users", t => t.sellerID)
                .ForeignKey("dbo.Users", t => t.buyerID)
                .Index(t => t.OfferID)
                .Index(t => t.buyerID)
                .Index(t => t.sellerID);
            
            CreateTable(
                "dbo.Offer",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ProductID = c.Long(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                        offerowner = c.Long(nullable: false),
                        Descreption = c.String(nullable: false, maxLength: 4000),
                        unit = c.String(nullable: false, maxLength: 50),
                        quntity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        currency = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .ForeignKey("dbo.Users", t => t.offerowner)
                .Index(t => t.ProductID)
                .Index(t => t.offerowner);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Liecnse = c.String(maxLength: 50),
                        UserID = c.Long(nullable: false),
                        Description = c.String(maxLength: 4000),
                        image = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Address = c.String(maxLength: 200),
                        Email = c.String(nullable: false, maxLength: 100),
                        RoleID = c.Long(nullable: false),
                        Liecnse = c.String(maxLength: 100),
                        NID = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 50),
                        CompanyName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Role", t => t.RoleID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleID = c.Long(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleID", "dbo.Role");
            DropForeignKey("dbo.Products", "UserID", "dbo.Users");
            DropForeignKey("dbo.Offer", "offerowner", "dbo.Users");
            DropForeignKey("dbo.Contracts", "buyerID", "dbo.Users");
            DropForeignKey("dbo.Contracts", "sellerID", "dbo.Users");
            DropForeignKey("dbo.Offer", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Contracts", "OfferID", "dbo.Offer");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.Products", new[] { "UserID" });
            DropIndex("dbo.Offer", new[] { "offerowner" });
            DropIndex("dbo.Offer", new[] { "ProductID" });
            DropIndex("dbo.Contracts", new[] { "sellerID" });
            DropIndex("dbo.Contracts", new[] { "buyerID" });
            DropIndex("dbo.Contracts", new[] { "OfferID" });
            DropTable("dbo.Role");
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Offer");
            DropTable("dbo.Contracts");
        }
    }
}
