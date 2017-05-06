namespace WildCampingWithMvc.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageNotRequiredInCategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DbSiteCategories", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DbSiteCategories", "Image", c => c.Binary(nullable: false));
        }
    }
}
