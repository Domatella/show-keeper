namespace TvShows.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseId);
            
            CreateTable(
                "dbo.ShowEpisodes",
                c => new
                    {
                        ShowEpisodeId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ShowId = c.Int(nullable: false),
                        Season = c.Int(nullable: false),
                        Episode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShowEpisodeId);
            
            CreateTable(
                "dbo.Shows",
                c => new
                    {
                        ShowId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Seasons = c.Int(nullable: false),
                        Series = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShowId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Shows");
            DropTable("dbo.ShowEpisodes");
            DropTable("dbo.Purchases");
        }
    }
}
