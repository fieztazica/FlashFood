namespace server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Order", newName: "Orders");
            DropForeignKey("dbo.Order", "Statuses_Id", "dbo.Status");
            DropIndex("dbo.Orders", new[] { "Statuses_Id" });
          
         
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "Statuses_Id", c => c.Byte());
            CreateIndex("dbo.Orders", "Statuses_Id");
            AddForeignKey("dbo.Order", "Statuses_Id", "dbo.Status", "Id");
            RenameTable(name: "dbo.Orders", newName: "Order");
        }
    }
}
