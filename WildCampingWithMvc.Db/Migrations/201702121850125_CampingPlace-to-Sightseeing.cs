namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampingPlacetoSightseeing : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sightseeings", "CampingPlace_Id", "dbo.CampingPlaces");
            DropIndex("dbo.Sightseeings", new[] { "CampingPlace_Id" });
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
            
            DropColumn("dbo.Sightseeings", "CampingPlace_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sightseeings", "CampingPlace_Id", c => c.Guid());
            DropForeignKey("dbo.SightseeingCampingPlaces", "CampingPlace_Id", "dbo.CampingPlaces");
            DropForeignKey("dbo.SightseeingCampingPlaces", "Sightseeing_Id", "dbo.Sightseeings");
            DropIndex("dbo.SightseeingCampingPlaces", new[] { "CampingPlace_Id" });
            DropIndex("dbo.SightseeingCampingPlaces", new[] { "Sightseeing_Id" });
            DropTable("dbo.SightseeingCampingPlaces");
            CreateIndex("dbo.Sightseeings", "CampingPlace_Id");
            AddForeignKey("dbo.Sightseeings", "CampingPlace_Id", "dbo.CampingPlaces", "Id");
        }
    }
}
