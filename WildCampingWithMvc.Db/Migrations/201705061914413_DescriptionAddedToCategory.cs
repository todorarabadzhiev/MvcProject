namespace WildCampingWithMvc.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescriptionAddedToCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DbSiteCategories", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DbSiteCategories", "Description");
        }
    }
}
