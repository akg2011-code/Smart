namespace SmartSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class akg : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductTypes", "Image", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductTypes", "Image", c => c.String());
        }
    }
}
