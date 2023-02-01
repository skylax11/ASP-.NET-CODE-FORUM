namespace letsDoThis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedGuid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ActivateGuid", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ActivateGuid");
        }
    }
}
