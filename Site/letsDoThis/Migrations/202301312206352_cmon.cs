namespace letsDoThis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cmon : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Post_PostID", c => c.Int());
            AddColumn("dbo.Posts", "owner_UserID", c => c.Int());
            AddForeignKey("dbo.Users", "Post_PostID", "dbo.Posts", "PostID");
        }
    }
}
