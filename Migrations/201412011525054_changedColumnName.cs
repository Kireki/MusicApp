namespace MusicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedColumnName : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        Email = c.String(),
                        FacebookUserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserArtists",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        ArtistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.ArtistID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.ArtistID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ArtistID);
            
            CreateTable(
                "dbo.UserTracks",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        TrackID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.TrackID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Tracks", t => t.TrackID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.TrackID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserTracks", "TrackID", "dbo.Tracks");
            DropForeignKey("dbo.UserTracks", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserArtists", "ArtistID", "dbo.Artists");
            DropForeignKey("dbo.UserArtists", "UserID", "dbo.Users");
            DropForeignKey("dbo.Tracks", "ArtistId", "dbo.Artists");
            DropIndex("dbo.UserTracks", new[] { "TrackID" });
            DropIndex("dbo.UserTracks", new[] { "UserID" });
            DropIndex("dbo.UserArtists", new[] { "ArtistID" });
            DropIndex("dbo.UserArtists", new[] { "UserID" });
            DropIndex("dbo.Tracks", new[] { "ArtistId" });
            DropTable("dbo.UserTracks");
            DropTable("dbo.UserArtists");
            DropTable("dbo.Users");
            DropTable("dbo.Tracks");
            DropTable("dbo.Artists");
        }
    }
}
