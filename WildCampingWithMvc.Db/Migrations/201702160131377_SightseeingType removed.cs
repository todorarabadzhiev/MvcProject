namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SightseeingTyperemoved : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sightseeings", "Type_Id", "dbo.SightseeingTypes");
            DropIndex("dbo.Sightseeings", new[] { "Type_Id" });
            DropColumn("dbo.Sightseeings", "Type_Id");
            DropTable("dbo.SightseeingTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SightseeingTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Sightseeings", "Type_Id", c => c.Guid());
            CreateIndex("dbo.Sightseeings", "Type_Id");
            AddForeignKey("dbo.Sightseeings", "Type_Id", "dbo.SightseeingTypes", "Id");
        }
    }
}
