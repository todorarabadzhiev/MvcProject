namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campinguserapplicationUserIdmadestring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CampingUsers", "ApplicationUserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CampingUsers", "ApplicationUserId", c => c.Guid(nullable: false));
        }
    }
}
