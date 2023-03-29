namespace server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ordertype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Type", c => c.String(nullable: false));
            DropColumn("dbo.Orders", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Status", c => c.String(nullable: false));
            DropColumn("dbo.Orders", "Type");
        }
    }
}
