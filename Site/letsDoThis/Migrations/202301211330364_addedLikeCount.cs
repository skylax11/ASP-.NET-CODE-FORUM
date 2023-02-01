namespace letsDoThis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedLikeCount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        CommentText = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Owner_UserID = c.Int(),
                        thePost_PostID = c.Int(),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.User", t => t.Owner_UserID)
                .ForeignKey("dbo.Post", t => t.thePost_PostID)
                .Index(t => t.Owner_UserID)
                .Index(t => t.thePost_PostID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        email = c.String(nullable: false, maxLength: 100),
                        ProfileImage = c.String(),
                        isAdmin = c.Boolean(nullable: false),
                        isActivated = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        Desc = c.String(nullable: false, maxLength: 200),
                        LikeCount = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        owner_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.User", t => t.owner_UserID)
                .Index(t => t.owner_UserID);
            
            CreateTable(
                "dbo.Like",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        likedPost_PostID = c.Int(),
                        likedUser_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Post", t => t.likedPost_PostID)
                .ForeignKey("dbo.User", t => t.likedUser_UserID)
                .Index(t => t.likedPost_PostID)
                .Index(t => t.likedUser_UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Post", "owner_UserID", "dbo.User");
            DropForeignKey("dbo.Like", "likedUser_UserID", "dbo.User");
            DropForeignKey("dbo.Like", "likedPost_PostID", "dbo.Post");
            DropForeignKey("dbo.Comment", "thePost_PostID", "dbo.Post");
            DropForeignKey("dbo.Comment", "Owner_UserID", "dbo.User");
            DropIndex("dbo.Like", new[] { "likedUser_UserID" });
            DropIndex("dbo.Like", new[] { "likedPost_PostID" });
            DropIndex("dbo.Post", new[] { "owner_UserID" });
            DropIndex("dbo.Comment", new[] { "thePost_PostID" });
            DropIndex("dbo.Comment", new[] { "Owner_UserID" });
            DropTable("dbo.Like");
            DropTable("dbo.Post");
            DropTable("dbo.User");
            DropTable("dbo.Comment");
        }
    }
}
