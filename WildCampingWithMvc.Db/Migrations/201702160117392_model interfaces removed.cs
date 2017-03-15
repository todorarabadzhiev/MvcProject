namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelinterfacesremoved : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.CampingPlaces", "CampingUser_Id", c => c.Guid());
            AddColumn("dbo.CampingPlaces", "CampingUser_Id1", c => c.Guid());
            AddColumn("dbo.CampingPlaces", "AddedBy_Id", c => c.Guid());
            AddColumn("dbo.Sightseeings", "Type_Id", c => c.Guid());
            CreateIndex("dbo.CampingPlaces", "CampingUser_Id");
            CreateIndex("dbo.CampingPlaces", "CampingUser_Id1");
            CreateIndex("dbo.CampingPlaces", "AddedBy_Id");
            CreateIndex("dbo.Sightseeings", "Type_Id");
            AddForeignKey("dbo.CampingPlaces", "CampingUser_Id", "dbo.CampingUsers", "Id");
            AddForeignKey("dbo.CampingPlaces", "CampingUser_Id1", "dbo.CampingUsers", "Id");
            AddForeignKey("dbo.CampingPlaces", "AddedBy_Id", "dbo.CampingUsers", "Id");
            AddForeignKey("dbo.Sightseeings", "Type_Id", "dbo.SightseeingTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiteCategoryCampingPlaces", "CampingPlace_Id", "dbo.CampingPlaces");
            DropForeignKey("dbo.SiteCategoryCampingPlaces", "SiteCategory_Id", "dbo.SiteCategories");
            DropForeignKey("dbo.Sightseeings", "Type_Id", "dbo.SightseeingTypes");
            DropForeignKey("dbo.SightseeingCampingPlaces", "CampingPlace_Id", "dbo.CampingPlaces");
            DropForeignKey("dbo.SightseeingCampingPlaces", "Sightseeing_Id", "dbo.Sightseeings");
            DropForeignKey("dbo.CampingPlaces", "AddedBy_Id", "dbo.CampingUsers");
            DropForeignKey("dbo.CampingPlaces", "CampingUser_Id1", "dbo.CampingUsers");
            DropForeignKey("dbo.CampingPlaces", "CampingUser_Id", "dbo.CampingUsers");
            DropIndex("dbo.SiteCategoryCampingPlaces", new[] { "CampingPlace_Id" });
            DropIndex("dbo.SiteCategoryCampingPlaces", new[] { "SiteCategory_Id" });
            DropIndex("dbo.SightseeingCampingPlaces", new[] { "CampingPlace_Id" });
            DropIndex("dbo.SightseeingCampingPlaces", new[] { "Sightseeing_Id" });
            DropIndex("dbo.Sightseeings", new[] { "Type_Id" });
            DropIndex("dbo.CampingPlaces", new[] { "AddedBy_Id" });
            DropIndex("dbo.CampingPlaces", new[] { "CampingUser_Id1" });
            DropIndex("dbo.CampingPlaces", new[] { "CampingUser_Id" });
            DropColumn("dbo.Sightseeings", "Type_Id");
            DropColumn("dbo.CampingPlaces", "AddedBy_Id");
            DropColumn("dbo.CampingPlaces", "CampingUser_Id1");
            DropColumn("dbo.CampingPlaces", "CampingUser_Id");
            DropTable("dbo.SiteCategoryCampingPlaces");
            DropTable("dbo.SightseeingCampingPlaces");
        }
    }
}
