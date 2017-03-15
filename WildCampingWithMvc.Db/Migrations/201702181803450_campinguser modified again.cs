namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campingusermodifiedagain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CampingUsers", "ApplicationUserId", c => c.Guid(nullable: false));
            DropColumn("dbo.CampingUsers", "AspNetUsersId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CampingUsers", "AspNetUsersId", c => c.Guid(nullable: false));
            DropColumn("dbo.CampingUsers", "ApplicationUserId");
        }
    }
}
