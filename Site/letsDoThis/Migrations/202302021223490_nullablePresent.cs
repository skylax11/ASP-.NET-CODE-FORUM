namespace letsDoThis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullablePresent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comment", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.Comment", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.Users", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.Users", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.Posts", "CreatedDate", c => c.DateTime());
            AlterColumn("dbo.Posts", "ModifiedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Posts", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Comment", "ModifiedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Comment", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
