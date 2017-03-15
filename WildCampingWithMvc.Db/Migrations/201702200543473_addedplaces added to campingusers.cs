namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedplacesaddedtocampingusers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CampingPlaces", "AddedBy_Id", "dbo.CampingUsers");
            AddColumn("dbo.CampingPlaces", "CampingUser_Id", c => c.Guid());
            AddColumn("dbo.CampingPlaces", "CampingUser_Id1", c => c.Guid());
            CreateIndex("dbo.CampingPlaces", "CampingUser_Id");
            CreateIndex("dbo.CampingPlaces", "CampingUser_Id1");
            AddForeignKey("dbo.CampingPlaces", "CampingUser_Id", "dbo.CampingUsers", "Id");
            AddForeignKey("dbo.CampingPlaces", "CampingUser_Id1", "dbo.CampingUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CampingPlaces", "CampingUser_Id1", "dbo.CampingUsers");
            DropForeignKey("dbo.CampingPlaces", "CampingUser_Id", "dbo.CampingUsers");
            DropIndex("dbo.CampingPlaces", new[] { "CampingUser_Id1" });
            DropIndex("dbo.CampingPlaces", new[] { "CampingUser_Id" });
            DropColumn("dbo.CampingPlaces", "CampingUser_Id1");
            DropColumn("dbo.CampingPlaces", "CampingUser_Id");
            AddForeignKey("dbo.CampingPlaces", "AddedBy_Id", "dbo.CampingUsers", "Id");
        }
    }
}
