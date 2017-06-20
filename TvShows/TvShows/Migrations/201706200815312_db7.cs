namespace TvShows.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Purchases", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UserSubscriptions", "UserId", c => c.String());
            DropColumn("dbo.Purchases", "IsPaid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Purchases", "IsPaid", c => c.Boolean(nullable: false));
            AlterColumn("dbo.UserSubscriptions", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Purchases", "IsActive");
        }
    }
}
