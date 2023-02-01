namespace letsDoThis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedTableNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.User", newName: "Users");
            RenameTable(name: "dbo.Like", newName: "Likes");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Likes", newName: "Like");
            RenameTable(name: "dbo.Users", newName: "User");
        }
    }
}
