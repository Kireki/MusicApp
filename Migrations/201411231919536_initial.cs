namespace MusicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
                "dbo.FacebookUsers",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                        Facebook = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tracks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ArtistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Artists", t => t.ArtistID, cascadeDelete: true)
                .Index(t => t.ArtistID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FacebookUserArtists",
                c => new
                    {
                        FacebookUserID = c.Int(nullable: false),
                        ArtistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FacebookUserID, t.ArtistID })
                .ForeignKey("dbo.FacebookUsers", t => t.FacebookUserID, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.ArtistID, cascadeDelete: true)
                .Index(t => t.FacebookUserID)
                .Index(t => t.ArtistID);
            
            CreateTable(
                "dbo.FacebookUserTracks",
                c => new
                    {
                        FacebookUserID = c.Int(nullable: false),
                        TrackID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FacebookUserID, t.TrackID })
                .ForeignKey("dbo.FacebookUsers", t => t.FacebookUserID, cascadeDelete: true)
                .ForeignKey("dbo.Tracks", t => t.TrackID, cascadeDelete: true)
                .Index(t => t.FacebookUserID)
                .Index(t => t.TrackID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FacebookUserTracks", "TrackID", "dbo.Tracks");
            DropForeignKey("dbo.FacebookUserTracks", "FacebookUserID", "dbo.FacebookUsers");
            DropForeignKey("dbo.Tracks", "ArtistID", "dbo.Artists");
            DropForeignKey("dbo.FacebookUserArtists", "ArtistID", "dbo.Artists");
            DropForeignKey("dbo.FacebookUserArtists", "FacebookUserID", "dbo.FacebookUsers");
            DropIndex("dbo.FacebookUserTracks", new[] { "TrackID" });
            DropIndex("dbo.FacebookUserTracks", new[] { "FacebookUserID" });
            DropIndex("dbo.FacebookUserArtists", new[] { "ArtistID" });
            DropIndex("dbo.FacebookUserArtists", new[] { "FacebookUserID" });
            DropIndex("dbo.Tracks", new[] { "ArtistID" });
            DropTable("dbo.FacebookUserTracks");
            DropTable("dbo.FacebookUserArtists");
            DropTable("dbo.Users");
            DropTable("dbo.Tracks");
            DropTable("dbo.FacebookUsers");
            DropTable("dbo.Artists");
        }
    }
}
