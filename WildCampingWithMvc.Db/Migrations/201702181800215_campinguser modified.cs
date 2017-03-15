namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campingusermodified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CampingUsers", "AspNetUsersId", c => c.Guid(nullable: false));
            DropColumn("dbo.CampingUsers", "RegisteredUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CampingUsers", "RegisteredUserId", c => c.Guid(nullable: false));
            DropColumn("dbo.CampingUsers", "AspNetUsersId");
        }
    }
}
