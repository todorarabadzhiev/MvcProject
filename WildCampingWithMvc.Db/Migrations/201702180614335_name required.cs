namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class namerequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CampingPlaces", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.CampingUsers", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Sightseeings", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.SiteCategories", "Name", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SiteCategories", "Name", c => c.String(maxLength: 30));
            AlterColumn("dbo.Sightseeings", "Name", c => c.String(maxLength: 30));
            AlterColumn("dbo.CampingUsers", "Name", c => c.String(maxLength: 30));
            AlterColumn("dbo.CampingPlaces", "Name", c => c.String(maxLength: 30));
        }
    }
}
