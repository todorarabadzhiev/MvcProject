namespace WildCampingWithMvc.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryIsDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DbSiteCategories", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DbSiteCategories", "IsDeleted");
        }
    }
}
