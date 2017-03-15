namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campingplacetouserrelationchanged : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CampingPlaces", name: "CampingUser_Id", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.CampingPlaces", name: "CampingUser_Id1", newName: "CampingUser_Id");
            RenameColumn(table: "dbo.CampingPlaces", name: "__mig_tmp__0", newName: "CampingUser_Id1");
            RenameIndex(table: "dbo.CampingPlaces", name: "IX_CampingUser_Id1", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.CampingPlaces", name: "IX_CampingUser_Id", newName: "IX_CampingUser_Id1");
            RenameIndex(table: "dbo.CampingPlaces", name: "__mig_tmp__0", newName: "IX_CampingUser_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CampingPlaces", name: "IX_CampingUser_Id", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.CampingPlaces", name: "IX_CampingUser_Id1", newName: "IX_CampingUser_Id");
            RenameIndex(table: "dbo.CampingPlaces", name: "__mig_tmp__0", newName: "IX_CampingUser_Id1");
            RenameColumn(table: "dbo.CampingPlaces", name: "CampingUser_Id1", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.CampingPlaces", name: "CampingUser_Id", newName: "CampingUser_Id1");
            RenameColumn(table: "dbo.CampingPlaces", name: "__mig_tmp__0", newName: "CampingUser_Id");
        }
    }
}
