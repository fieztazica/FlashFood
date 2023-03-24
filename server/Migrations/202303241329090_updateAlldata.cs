namespace server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAlldata : DbMigration
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
                .Index(t => t.MealId)
                .Index(t => t.UserId);
            AddForeignKey("dbo.CartItems", "UserId", "dbo.AspNetUsers", "Id");
            CreateTable(
                "dbo.OderItems",
                c => new
                    {
                        MealId = c.Int(nullable: false),
                        OderId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.MealId, t.OderId })
                .ForeignKey("dbo.Meals", t => t.MealId, cascadeDelete: true)
                .ForeignKey("dbo.Oder", t => t.OderId, cascadeDelete: true)
                .Index(t => t.MealId)
                .Index(t => t.OderId);
            
            CreateTable(
                "dbo.Oder",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OderAt = c.DateTime(nullable: false),
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
            AddForeignKey("dbo.Oder", "SellerId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Oder", "UserId", "dbo.AspNetUsers", "Id");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.CartItems", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.CartItems", "MealId", "dbo.Meals");
            DropForeignKey("dbo.OderItems", "OderId", "dbo.Oder");
            DropForeignKey("dbo.Oder", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Oder", "SellerId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.OderItems", "MealId", "dbo.Meals");
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Oder", new[] { "UserId" });
            DropIndex("dbo.Oder", new[] { "SellerId" });
            DropIndex("dbo.OderItems", new[] { "OderId" });
            DropIndex("dbo.OderItems", new[] { "MealId" });
            DropIndex("dbo.CartItems", new[] { "UserId" });
            DropIndex("dbo.CartItems", new[] { "MealId" });
            DropPrimaryKey("dbo.IdentityUserLogins");
            AlterColumn("dbo.IdentityUserLogins", "ProviderKey", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.IdentityUserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ApplicationUsers", "UserName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.ApplicationUsers", "Email", c => c.String(maxLength: 256));
            AlterColumn("dbo.IdentityRoles", "Name", c => c.String(nullable: false, maxLength: 256));
            DropColumn("dbo.IdentityUserLogins", "ApplicationUser_Id");
            DropColumn("dbo.IdentityUserClaims", "ApplicationUser_Id");
            DropColumn("dbo.IdentityUserRoles", "IdentityRole_Id");
            DropColumn("dbo.IdentityUserRoles", "ApplicationUser_Id");
            DropTable("dbo.Oder");
            DropTable("dbo.OderItems");
            DropTable("dbo.CartItems");
            AddPrimaryKey("dbo.IdentityUserLogins", new[] { "LoginProvider", "ProviderKey", "UserId" });
            CreateIndex("dbo.IdentityUserLogins", "UserId");
            CreateIndex("dbo.IdentityUserClaims", "UserId");
            CreateIndex("dbo.ApplicationUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.IdentityUserRoles", "RoleId");
            CreateIndex("dbo.IdentityUserRoles", "UserId");
            CreateIndex("dbo.IdentityRoles", "Name", unique: true, name: "RoleNameIndex");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.IdentityUserLogins", newName: "AspNetUserLogins");
            RenameTable(name: "dbo.IdentityUserClaims", newName: "AspNetUserClaims");
            RenameTable(name: "dbo.ApplicationUsers", newName: "AspNetUsers");
            RenameTable(name: "dbo.IdentityUserRoles", newName: "AspNetUserRoles");
            RenameTable(name: "dbo.IdentityRoles", newName: "AspNetRoles");
        }
    }
}
