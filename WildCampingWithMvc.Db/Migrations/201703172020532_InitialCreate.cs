namespace WildCampingWithMvc.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DbCampingPlaces",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(),
                        GoogleMapsUrl = c.String(),
                        WaterOnSite = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        AddedOn = c.DateTime(nullable: false),
                        AddedById = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbCampingUsers", t => t.AddedById)
                .Index(t => t.AddedById);
            
            CreateTable(
                "dbo.DbCampingUsers",
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
                "dbo.DbImageFiles",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Data = c.Binary(),
                        FileName = c.String(),
                        DbCampingPlaceId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DbCampingPlaces", t => t.DbCampingPlaceId)
                .Index(t => t.DbCampingPlaceId);
            
            CreateTable(
                "dbo.DbSightseeings",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DbSiteCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DbSightseeingDbCampingPlaces",
                c => new
                    {
                        DbSightseeing_Id = c.Guid(nullable: false),
                        DbCampingPlace_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbSightseeing_Id, t.DbCampingPlace_Id })
                .ForeignKey("dbo.DbSightseeings", t => t.DbSightseeing_Id, cascadeDelete: true)
                .ForeignKey("dbo.DbCampingPlaces", t => t.DbCampingPlace_Id, cascadeDelete: true)
                .Index(t => t.DbSightseeing_Id)
                .Index(t => t.DbCampingPlace_Id);
            
            CreateTable(
                "dbo.DbSiteCategoryDbCampingPlaces",
                c => new
                    {
                        DbSiteCategory_Id = c.Guid(nullable: false),
                        DbCampingPlace_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbSiteCategory_Id, t.DbCampingPlace_Id })
                .ForeignKey("dbo.DbSiteCategories", t => t.DbSiteCategory_Id, cascadeDelete: true)
                .ForeignKey("dbo.DbCampingPlaces", t => t.DbCampingPlace_Id, cascadeDelete: true)
                .Index(t => t.DbSiteCategory_Id)
                .Index(t => t.DbCampingPlace_Id);
            
            CreateTable(
                "dbo.DbCampingPlaceDbCampingUsers",
                c => new
                    {
                        DbCampingPlace_Id = c.Guid(nullable: false),
                        DbCampingUser_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbCampingPlace_Id, t.DbCampingUser_Id })
                .ForeignKey("dbo.DbCampingPlaces", t => t.DbCampingPlace_Id, cascadeDelete: true)
                .ForeignKey("dbo.DbCampingUsers", t => t.DbCampingUser_Id, cascadeDelete: true)
                .Index(t => t.DbCampingPlace_Id)
                .Index(t => t.DbCampingUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DbCampingPlaceDbCampingUsers", "DbCampingUser_Id", "dbo.DbCampingUsers");
            DropForeignKey("dbo.DbCampingPlaceDbCampingUsers", "DbCampingPlace_Id", "dbo.DbCampingPlaces");
            DropForeignKey("dbo.DbSiteCategoryDbCampingPlaces", "DbCampingPlace_Id", "dbo.DbCampingPlaces");
            DropForeignKey("dbo.DbSiteCategoryDbCampingPlaces", "DbSiteCategory_Id", "dbo.DbSiteCategories");
            DropForeignKey("dbo.DbSightseeingDbCampingPlaces", "DbCampingPlace_Id", "dbo.DbCampingPlaces");
            DropForeignKey("dbo.DbSightseeingDbCampingPlaces", "DbSightseeing_Id", "dbo.DbSightseeings");
            DropForeignKey("dbo.DbImageFiles", "DbCampingPlaceId", "dbo.DbCampingPlaces");
            DropForeignKey("dbo.DbCampingPlaces", "AddedById", "dbo.DbCampingUsers");
            DropIndex("dbo.DbCampingPlaceDbCampingUsers", new[] { "DbCampingUser_Id" });
            DropIndex("dbo.DbCampingPlaceDbCampingUsers", new[] { "DbCampingPlace_Id" });
            DropIndex("dbo.DbSiteCategoryDbCampingPlaces", new[] { "DbCampingPlace_Id" });
            DropIndex("dbo.DbSiteCategoryDbCampingPlaces", new[] { "DbSiteCategory_Id" });
            DropIndex("dbo.DbSightseeingDbCampingPlaces", new[] { "DbCampingPlace_Id" });
            DropIndex("dbo.DbSightseeingDbCampingPlaces", new[] { "DbSightseeing_Id" });
            DropIndex("dbo.DbImageFiles", new[] { "DbCampingPlaceId" });
            DropIndex("dbo.DbCampingPlaces", new[] { "AddedById" });
            DropTable("dbo.DbCampingPlaceDbCampingUsers");
            DropTable("dbo.DbSiteCategoryDbCampingPlaces");
            DropTable("dbo.DbSightseeingDbCampingPlaces");
            DropTable("dbo.DbSiteCategories");
            DropTable("dbo.DbSightseeings");
            DropTable("dbo.DbImageFiles");
            DropTable("dbo.DbCampingUsers");
            DropTable("dbo.DbCampingPlaces");
        }
    }
}
