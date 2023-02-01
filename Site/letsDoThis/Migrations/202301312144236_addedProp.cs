namespace letsDoThis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Post_PostID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "Post_PostID");
        }
    }
}
