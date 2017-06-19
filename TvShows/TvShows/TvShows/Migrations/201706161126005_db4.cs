namespace TvShows.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db4 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Shows", "CountryId");
            AddForeignKey("dbo.Shows", "CountryId", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shows", "CountryId", "dbo.Countries");
            DropIndex("dbo.Shows", new[] { "CountryId" });
        }
    }
}
