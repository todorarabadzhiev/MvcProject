namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fieldnamespreadjusted : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Sightseeings", name: "SightseeingType_Id", newName: "Type_Id");
            RenameIndex(table: "dbo.Sightseeings", name: "IX_SightseeingType_Id", newName: "IX_Type_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Sightseeings", name: "IX_Type_Id", newName: "IX_SightseeingType_Id");
            RenameColumn(table: "dbo.Sightseeings", name: "Type_Id", newName: "SightseeingType_Id");
        }
    }
}
