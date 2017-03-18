namespace WildCampingWithMvc.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuthInDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DbCampingPlaces", "DbCampingUser_Id", "dbo.DbCampingUsers");
            DropForeignKey("dbo.DbCampingPlaces", "DbCampingUser_Id1", "dbo.DbCampingUsers");
            DropIndex("dbo.DbCampingPlaces", new[] { "DbCampingUser_Id" });
            DropIndex("dbo.DbCampingPlaces", new[] { "DbCampingUser_Id1" });
            DropIndex("dbo.DbCampingPlaces", new[] { "AddedBy_Id" });
            DropIndex("dbo.DbImageFiles", new[] { "DbCampingPlace_Id" });
            RenameColumn(table: "dbo.DbCampingPlaces", name: "AddedBy_Id", newName: "AddedById");
            RenameColumn(table: "dbo.DbImageFiles", name: "DbCampingPlace_Id", newName: "DbCampingPlaceId");
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
            
            AlterColumn("dbo.DbCampingPlaces", "AddedById", c => c.Guid(nullable: false));
            AlterColumn("dbo.DbImageFiles", "DbCampingPlaceId", c => c.Guid(nullable: false));
            CreateIndex("dbo.DbCampingPlaces", "AddedById");
            CreateIndex("dbo.DbImageFiles", "DbCampingPlaceId");
            DropColumn("dbo.DbCampingPlaces", "DbCampingUser_Id");
            DropColumn("dbo.DbCampingPlaces", "DbCampingUser_Id1");
            DropColumn("dbo.DbImageFiles", "CampingPlaceId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DbImageFiles", "CampingPlaceId", c => c.Guid(nullable: false));
            AddColumn("dbo.DbCampingPlaces", "DbCampingUser_Id1", c => c.Guid());
            AddColumn("dbo.DbCampingPlaces", "DbCampingUser_Id", c => c.Guid());
            DropForeignKey("dbo.DbCampingPlaceDbCampingUsers", "DbCampingUser_Id", "dbo.DbCampingUsers");
            DropForeignKey("dbo.DbCampingPlaceDbCampingUsers", "DbCampingPlace_Id", "dbo.DbCampingPlaces");
            DropIndex("dbo.DbCampingPlaceDbCampingUsers", new[] { "DbCampingUser_Id" });
            DropIndex("dbo.DbCampingPlaceDbCampingUsers", new[] { "DbCampingPlace_Id" });
            DropIndex("dbo.DbImageFiles", new[] { "DbCampingPlaceId" });
            DropIndex("dbo.DbCampingPlaces", new[] { "AddedById" });
            AlterColumn("dbo.DbImageFiles", "DbCampingPlaceId", c => c.Guid());
            AlterColumn("dbo.DbCampingPlaces", "AddedById", c => c.Guid());
            DropTable("dbo.DbCampingPlaceDbCampingUsers");
            RenameColumn(table: "dbo.DbImageFiles", name: "DbCampingPlaceId", newName: "DbCampingPlace_Id");
            RenameColumn(table: "dbo.DbCampingPlaces", name: "AddedById", newName: "AddedBy_Id");
            CreateIndex("dbo.DbImageFiles", "DbCampingPlace_Id");
            CreateIndex("dbo.DbCampingPlaces", "AddedBy_Id");
            CreateIndex("dbo.DbCampingPlaces", "DbCampingUser_Id1");
            CreateIndex("dbo.DbCampingPlaces", "DbCampingUser_Id");
            AddForeignKey("dbo.DbCampingPlaces", "DbCampingUser_Id1", "dbo.DbCampingUsers", "Id");
            AddForeignKey("dbo.DbCampingPlaces", "DbCampingUser_Id", "dbo.DbCampingUsers", "Id");
        }
    }
}
