namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isdeletedaddedtocampingplaces : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CampingPlaces", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CampingPlaces", "IsDeleted");
        }
    }
}
