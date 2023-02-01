namespace letsDoThis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sizeIncreased : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Post", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Post", "Desc", c => c.String(nullable: false, maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Post", "Desc", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Post", "Title", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
