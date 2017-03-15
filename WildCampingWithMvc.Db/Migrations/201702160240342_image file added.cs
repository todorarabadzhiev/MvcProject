namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imagefileadded : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageFiles", "CampingPlaceId", "dbo.CampingPlaces");
            DropIndex("dbo.ImageFiles", new[] { "CampingPlaceId" });
            DropTable("dbo.ImageFiles");
        }
    }
}
