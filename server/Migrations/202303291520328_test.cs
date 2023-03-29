namespace server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "PaidAt", c => c.DateTime());
            AlterColumn("dbo.Orders", "Paid", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Paid", c => c.Double(nullable: false));
            AlterColumn("dbo.Orders", "PaidAt", c => c.DateTime(nullable: false));
        }
    }
}
