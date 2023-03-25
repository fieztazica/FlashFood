namespace server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_DB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CartItems",
                c => new
                    {
                        MealId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.MealId, t.UserId })
                .ForeignKey("dbo.Meals", t => t.MealId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MealId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250),
                        AmountLeft = c.Int(nullable: false),
                        ImageURL = c.String(nullable: false, maxLength: 250),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        MealId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.MealId, t.OrderId })
                .ForeignKey("dbo.Meals", t => t.MealId, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.MealId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderAt = c.DateTime(nullable: false),
                        PaidAt = c.DateTime(nullable: false),
                        Total_money = c.Double(nullable: false),
                        Paid = c.Double(nullable: false),
                        Change = c.Double(nullable: false),
                        SellerId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                 .PrimaryKey(t => t.Id)
                .Index(t => t.SellerId)
                .Index(t => t.UserId);
            AddForeignKey("dbo.Order", "SellerId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Order", "UserId", "dbo.AspNetUsers", "Id");

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        AspNetUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUser_Id)
                .Index(t => t.AspNetUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(),
                        AspNetUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider })
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUser_Id)
                .Index(t => t.AspNetUser_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        AspNetUser_Id = c.String(maxLength: 128),
                        AspNetRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUser_Id)
                .ForeignKey("dbo.AspNetRoles", t => t.AspNetRole_Id)
                .Index(t => t.AspNetUser_Id)
                .Index(t => t.AspNetRole_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "AspNetRole_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.CartItems", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CartItems", "MealId", "dbo.Meals");
            DropForeignKey("dbo.OrderItems", "OrderId", "dbo.Order");
            DropForeignKey("dbo.Order", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Order", "SellerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "AspNetUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "AspNetUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "AspNetUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderItems", "MealId", "dbo.Meals");
            DropIndex("dbo.AspNetUserRoles", new[] { "AspNetRole_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "AspNetUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "AspNetUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "AspNetUser_Id" });
            DropIndex("dbo.Order", new[] { "UserId" });
            DropIndex("dbo.Order", new[] { "SellerId" });
            DropIndex("dbo.OrderItems", new[] { "OrderId" });
            DropIndex("dbo.OrderItems", new[] { "MealId" });
            DropIndex("dbo.CartItems", new[] { "UserId" });
            DropIndex("dbo.CartItems", new[] { "MealId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Order");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Meals");
            DropTable("dbo.CartItems");
        }
    }
}
