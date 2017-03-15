namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelinterfacesadded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CampingPlaces", "CampingUser_Id", "dbo.CampingUsers");
            DropForeignKey("dbo.CampingPlaces", "CampingUser_Id1", "dbo.CampingUsers");
            DropForeignKey("dbo.CampingPlaces", "AddedBy_Id", "dbo.CampingUsers");
            DropForeignKey("dbo.SightseeingCampingPlaces", "Sightseeing_Id", "dbo.Sightseeings");
            DropForeignKey("dbo.SightseeingCampingPlaces", "CampingPlace_Id", "dbo.CampingPlaces");
            DropForeignKey("dbo.Sightseeings", "Type_Id", "dbo.SightseeingTypes");
            DropForeignKey("dbo.SiteCategoryCampingPlaces", "SiteCategory_Id", "dbo.SiteCategories");
            DropForeignKey("dbo.SiteCategoryCampingPlaces", "CampingPlace_Id", "dbo.CampingPlaces");
            DropIndex("dbo.CampingPlaces", new[] { "CampingUser_Id" });
            DropIndex("dbo.CampingPlaces", new[] { "CampingUser_Id1" });
            DropIndex("dbo.CampingPlaces", new[] { "AddedBy_Id" });
            DropIndex("dbo.Sightseeings", new[] { "Type_Id" });
            DropIndex("dbo.SightseeingCampingPlaces", new[] { "Sightseeing_Id" });
            DropIndex("dbo.SightseeingCampingPlaces", new[] { "CampingPlace_Id" });
            DropIndex("dbo.SiteCategoryCampingPlaces", new[] { "SiteCategory_Id" });
            DropIndex("dbo.SiteCategoryCampingPlaces", new[] { "CampingPlace_Id" });
            DropColumn("dbo.CampingPlaces", "CampingUser_Id");
            DropColumn("dbo.CampingPlaces", "CampingUser_Id1");
            DropColumn("dbo.CampingPlaces", "AddedBy_Id");
            DropColumn("dbo.Sightseeings", "Type_Id");
            DropTable("dbo.SightseeingCampingPlaces");
            DropTable("dbo.SiteCategoryCampingPlaces");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SiteCategoryCampingPlaces",
                c => new
                    {
                        SiteCategory_Id = c.Guid(nullable: false),
                        CampingPlace_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SiteCategory_Id, t.CampingPlace_Id });
            
            CreateTable(
                "dbo.SightseeingCampingPlaces",
                c => new
                    {
                        Sightseeing_Id = c.Guid(nullable: false),
                        CampingPlace_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sightseeing_Id, t.CampingPlace_Id });
            
            AddColumn("dbo.Sightseeings", "Type_Id", c => c.Guid());
            AddColumn("dbo.CampingPlaces", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.CampingPlaces", "CampingUser_Id1", c => c.Guid());
            AddColumn("dbo.CampingPlaces", "CampingUser_Id", c => c.Guid());
            CreateIndex("dbo.SiteCategoryCampingPlaces", "CampingPlace_Id");
            CreateIndex("dbo.SiteCategoryCampingPlaces", "SiteCategory_Id");
            CreateIndex("dbo.SightseeingCampingPlaces", "CampingPlace_Id");
            CreateIndex("dbo.SightseeingCampingPlaces", "Sightseeing_Id");
            CreateIndex("dbo.Sightseeings", "Type_Id");
            CreateIndex("dbo.CampingPlaces", "AddedBy_Id");
            CreateIndex("dbo.CampingPlaces", "CampingUser_Id1");
            CreateIndex("dbo.CampingPlaces", "CampingUser_Id");
            AddForeignKey("dbo.SiteCategoryCampingPlaces", "CampingPlace_Id", "dbo.CampingPlaces", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SiteCategoryCampingPlaces", "SiteCategory_Id", "dbo.SiteCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Sightseeings", "Type_Id", "dbo.SightseeingTypes", "Id");
            AddForeignKey("dbo.SightseeingCampingPlaces", "CampingPlace_Id", "dbo.CampingPlaces", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SightseeingCampingPlaces", "Sightseeing_Id", "dbo.Sightseeings", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CampingPlaces", "AddedBy_Id", "dbo.CampingUsers", "Id");
            AddForeignKey("dbo.CampingPlaces", "CampingUser_Id1", "dbo.CampingUsers", "Id");
            AddForeignKey("dbo.CampingPlaces", "CampingUser_Id", "dbo.CampingUsers", "Id");
        }
    }
}
