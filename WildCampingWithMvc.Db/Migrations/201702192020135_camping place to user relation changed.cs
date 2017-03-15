namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campingplacetouserrelationchanged : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CampingPlaces", "CampingUser_Id", "dbo.CampingUsers");
            DropIndex("dbo.CampingPlaces", new[] { "CampingUser_Id" });
            DropIndex("dbo.CampingPlaces", new[] { "CampingUser_Id1" });
            DropColumn("dbo.CampingPlaces", "AddedBy_Id");
            RenameColumn(table: "dbo.CampingPlaces", name: "CampingUser_Id1", newName: "AddedBy_Id");
            DropColumn("dbo.CampingPlaces", "CampingUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CampingPlaces", "CampingUser_Id", c => c.Guid());
            RenameColumn(table: "dbo.CampingPlaces", name: "AddedBy_Id", newName: "CampingUser_Id1");
            AddColumn("dbo.CampingPlaces", "AddedBy_Id", c => c.Guid());
            CreateIndex("dbo.CampingPlaces", "CampingUser_Id1");
            CreateIndex("dbo.CampingPlaces", "CampingUser_Id");
            AddForeignKey("dbo.CampingPlaces", "CampingUser_Id", "dbo.CampingUsers", "Id");
        }
    }
}
