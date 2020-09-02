namespace SmartSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editProductTypeAddImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductTypes", "Image", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductTypes", "Image");
        }
    }
}
