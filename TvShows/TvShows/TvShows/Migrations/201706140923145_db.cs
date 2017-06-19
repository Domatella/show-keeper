namespace TvShows.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Purchases", "UserId");
            DropColumn("dbo.ShowEpisodes", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShowEpisodes", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Purchases", "UserId", c => c.Int(nullable: false));
        }
    }
}
