namespace server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addnewmeal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Meals", "Name", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Meals", "Name");
        }
    }
}
