namespace letsDoThis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class restardd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "Post_PostID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Post_PostID", c => c.Int(nullable: false));
        }
    }
}
