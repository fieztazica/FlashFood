namespace server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateMeal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meals", "ImageURL", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.Meals", "Type", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meals", "Type");
            DropColumn("dbo.Meals", "ImageURL");
        }
    }
}
