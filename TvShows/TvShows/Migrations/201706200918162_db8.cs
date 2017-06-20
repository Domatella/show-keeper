namespace TvShows.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "ItemsQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Purchases", "ItemsQuantity");
        }
    }
}
