namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CampingPlaces",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(),
                        GoogleMapsUrl = c.String(),
                        WaterOnSite = c.Boolean(nullable: false),
                        AddedOn = c.DateTime(nullable: false),
                        AddedBy_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CampingUsers", t => t.AddedBy_Id)
                .Index(t => t.AddedBy_Id);
            
            CreateTable(
                "dbo.CampingUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        ApplicationUserId = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        UserName = c.String(nullable: false, maxLength: 30),
                        RegisteredOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImageFiles",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Data = c.Binary(),
                        FileName = c.String(),
                        CampingPlaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CampingPlaces", t => t.CampingPlaceId)
                .Index(t => t.CampingPlaceId);
            
            CreateTable(
                "dbo.Sightseeings",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SiteCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SightseeingCampingPlaces",
                c => new
                    {
                        Sightseeing_Id = c.Guid(nullable: false),
                        CampingPlace_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sightseeing_Id, t.CampingPlace_Id })
                .ForeignKey("dbo.Sightseeings", t => t.Sightseeing_Id, cascadeDelete: true)
                .ForeignKey("dbo.CampingPlaces", t => t.CampingPlace_Id, cascadeDelete: true)
                .Index(t => t.Sightseeing_Id)
                .Index(t => t.CampingPlace_Id);
            
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
            DropForeignKey("dbo.SightseeingCampingPlaces", "CampingPlace_Id", "dbo.CampingPlaces");
            DropForeignKey("dbo.SightseeingCampingPlaces", "Sightseeing_Id", "dbo.Sightseeings");
            DropForeignKey("dbo.ImageFiles", "CampingPlaceId", "dbo.CampingPlaces");
            DropForeignKey("dbo.CampingPlaces", "AddedBy_Id", "dbo.CampingUsers");
            DropIndex("dbo.SiteCategoryCampingPlaces", new[] { "CampingPlace_Id" });
            DropIndex("dbo.SiteCategoryCampingPlaces", new[] { "SiteCategory_Id" });
            DropIndex("dbo.SightseeingCampingPlaces", new[] { "CampingPlace_Id" });
            DropIndex("dbo.SightseeingCampingPlaces", new[] { "Sightseeing_Id" });
            DropIndex("dbo.ImageFiles", new[] { "CampingPlaceId" });
            DropIndex("dbo.CampingPlaces", new[] { "AddedBy_Id" });
            DropTable("dbo.SiteCategoryCampingPlaces");
            DropTable("dbo.SightseeingCampingPlaces");
            DropTable("dbo.SiteCategories");
            DropTable("dbo.Sightseeings");
            DropTable("dbo.ImageFiles");
            DropTable("dbo.CampingUsers");
            DropTable("dbo.CampingPlaces");
        }
    }
}
