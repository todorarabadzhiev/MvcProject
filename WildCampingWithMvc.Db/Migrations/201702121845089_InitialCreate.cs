namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CampingPlaces",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Description = c.String(),
                        ClosestTown = c.String(),
                        GoogleMapsUrl = c.String(),
                        WaterOnSite = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sightseeings",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Type_Id = c.Guid(),
                        CampingPlace_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SightseeingTypes", t => t.Type_Id)
                .ForeignKey("dbo.CampingPlaces", t => t.CampingPlace_Id)
                .Index(t => t.Type_Id)
                .Index(t => t.CampingPlace_Id);
            
            CreateTable(
                "dbo.SightseeingTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SiteCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SiteCategoryCampingPlaces",
                c => new
                    {
                        SiteCategory_Id = c.Guid(nullable: false),
                        CampingPlace_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SiteCategory_Id, t.CampingPlace_Id })
                .ForeignKey("dbo.SiteCategories", t => t.SiteCategory_Id, cascadeDelete: true)
                .ForeignKey("dbo.CampingPlaces", t => t.CampingPlace_Id, cascadeDelete: true)
                .Index(t => t.SiteCategory_Id)
                .Index(t => t.CampingPlace_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiteCategoryCampingPlaces", "CampingPlace_Id", "dbo.CampingPlaces");
            DropForeignKey("dbo.SiteCategoryCampingPlaces", "SiteCategory_Id", "dbo.SiteCategories");
            DropForeignKey("dbo.Sightseeings", "CampingPlace_Id", "dbo.CampingPlaces");
            DropForeignKey("dbo.Sightseeings", "Type_Id", "dbo.SightseeingTypes");
            DropIndex("dbo.SiteCategoryCampingPlaces", new[] { "CampingPlace_Id" });
            DropIndex("dbo.SiteCategoryCampingPlaces", new[] { "SiteCategory_Id" });
            DropIndex("dbo.Sightseeings", new[] { "CampingPlace_Id" });
            DropIndex("dbo.Sightseeings", new[] { "Type_Id" });
            DropTable("dbo.SiteCategoryCampingPlaces");
            DropTable("dbo.SiteCategories");
            DropTable("dbo.SightseeingTypes");
            DropTable("dbo.Sightseeings");
            DropTable("dbo.CampingPlaces");
        }
    }
}
