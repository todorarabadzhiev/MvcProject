namespace WildCampingWithMvc.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sightseeingsupdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DbSightseeings", "Description", c => c.String(maxLength: 500));
            AddColumn("dbo.DbSightseeings", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.DbSightseeings", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DbSightseeings", "Image");
            DropColumn("dbo.DbSightseeings", "IsDeleted");
            DropColumn("dbo.DbSightseeings", "Description");
        }
    }
}
