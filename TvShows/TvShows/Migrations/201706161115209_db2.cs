namespace TvShows.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Shows", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.Shows", "Description", c => c.String());
            AddColumn("dbo.Shows", "CountryId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shows", "CountryId");
            DropColumn("dbo.Shows", "Description");
            DropColumn("dbo.Shows", "Year");
            DropTable("dbo.Countries");
        }
    }
}
