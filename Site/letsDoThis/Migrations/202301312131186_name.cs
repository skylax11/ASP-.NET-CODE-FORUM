namespace letsDoThis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class name : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Post", newName: "Posts");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Posts", newName: "Post");
        }
    }
}
