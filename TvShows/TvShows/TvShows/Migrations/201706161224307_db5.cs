namespace TvShows.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Shows", "CountryId", "dbo.Countries");
            DropIndex("dbo.Shows", new[] { "CountryId" });
            DropColumn("dbo.Shows", "CountryId");
            DropTable("dbo.Countries");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Shows", "CountryId", c => c.Int());
            CreateIndex("dbo.Shows", "CountryId");
            AddForeignKey("dbo.Shows", "CountryId", "dbo.Countries", "Id");
        }
    }
}
