namespace server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CartItems", "Amount", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderItems", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderItems", "Amount", c => c.Double(nullable: false));
            AlterColumn("dbo.CartItems", "Amount", c => c.Double(nullable: false));
        }
    }
}
