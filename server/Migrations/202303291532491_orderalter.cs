namespace server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderalter : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "Paid", c => c.Double());
            AlterColumn("dbo.Orders", "Change", c => c.Double());
            DropColumn("dbo.Orders", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Change", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "Paid", c => c.Double(nullable: false));
        }
    }
}
