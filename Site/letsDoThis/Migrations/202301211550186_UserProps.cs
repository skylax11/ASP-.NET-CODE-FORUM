namespace letsDoThis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserProps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "isActivate", c => c.Boolean(nullable: false));
            DropColumn("dbo.User", "isActivated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "isActivated", c => c.Boolean(nullable: false));
            DropColumn("dbo.User", "isActivate");
        }
    }
}
