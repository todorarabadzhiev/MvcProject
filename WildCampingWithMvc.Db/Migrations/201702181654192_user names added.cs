namespace CampingDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usernamesadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CampingUsers", "FirstName", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.CampingUsers", "LastName", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.CampingUsers", "UserName", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.CampingUsers", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CampingUsers", "Name", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.CampingUsers", "UserName");
            DropColumn("dbo.CampingUsers", "LastName");
            DropColumn("dbo.CampingUsers", "FirstName");
        }
    }
}
