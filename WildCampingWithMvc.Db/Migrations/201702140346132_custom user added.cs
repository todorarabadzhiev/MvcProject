namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customuseradded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CampingUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        RegisteredUserId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 30),
                        RegisteredOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CampingPlaces", "AddedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.CampingPlaces", "CampingUser_Id", c => c.Guid());
            AddColumn("dbo.CampingPlaces", "CampingUser_Id1", c => c.Guid());
            AddColumn("dbo.CampingPlaces", "AddedBy_Id", c => c.Guid());
            CreateIndex("dbo.CampingPlaces", "CampingUser_Id");
            CreateIndex("dbo.CampingPlaces", "CampingUser_Id1");
            CreateIndex("dbo.CampingPlaces", "AddedBy_Id");
            AddForeignKey("dbo.CampingPlaces", "CampingUser_Id", "dbo.CampingUsers", "Id");
            AddForeignKey("dbo.CampingPlaces", "CampingUser_Id1", "dbo.CampingUsers", "Id");
            AddForeignKey("dbo.CampingPlaces", "AddedBy_Id", "dbo.CampingUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CampingPlaces", "AddedBy_Id", "dbo.CampingUsers");
            DropForeignKey("dbo.CampingPlaces", "CampingUser_Id1", "dbo.CampingUsers");
            DropForeignKey("dbo.CampingPlaces", "CampingUser_Id", "dbo.CampingUsers");
            DropIndex("dbo.CampingPlaces", new[] { "AddedBy_Id" });
            DropIndex("dbo.CampingPlaces", new[] { "CampingUser_Id1" });
            DropIndex("dbo.CampingPlaces", new[] { "CampingUser_Id" });
            DropColumn("dbo.CampingPlaces", "AddedBy_Id");
            DropColumn("dbo.CampingPlaces", "CampingUser_Id1");
            DropColumn("dbo.CampingPlaces", "CampingUser_Id");
            DropColumn("dbo.CampingPlaces", "AddedOn");
            DropTable("dbo.CampingUsers");
        }
    }
}
