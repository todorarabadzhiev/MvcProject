namespace WildCampingWithMvc.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageAddedToCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DbSiteCategories", "Image", c => c.Binary(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DbSiteCategories", "Image");
        }
    }
}
