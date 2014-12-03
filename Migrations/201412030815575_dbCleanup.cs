namespace MusicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbCleanup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tracks", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.UserArtists", "ArtistID", "dbo.Artists");
            DropForeignKey("dbo.UserTracks", "TrackID", "dbo.Tracks");
            DropPrimaryKey("dbo.Artists");
            DropPrimaryKey("dbo.Tracks");
            AddColumn("dbo.Users", "FacebookName", c => c.String());
            AlterColumn("dbo.Artists", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Tracks", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Artists", "Id");
            AddPrimaryKey("dbo.Tracks", "Id");
            AddForeignKey("dbo.Tracks", "ArtistId", "dbo.Artists", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserArtists", "ArtistID", "dbo.Artists", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserTracks", "TrackID", "dbo.Tracks", "Id", cascadeDelete: true);
            DropColumn("dbo.Users", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Email", c => c.String());
            DropForeignKey("dbo.UserTracks", "TrackID", "dbo.Tracks");
            DropForeignKey("dbo.UserArtists", "ArtistID", "dbo.Artists");
            DropForeignKey("dbo.Tracks", "ArtistId", "dbo.Artists");
            DropPrimaryKey("dbo.Tracks");
            DropPrimaryKey("dbo.Artists");
            AlterColumn("dbo.Tracks", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Artists", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Users", "FacebookName");
            AddPrimaryKey("dbo.Tracks", "Id");
            AddPrimaryKey("dbo.Artists", "ID");
            AddForeignKey("dbo.UserTracks", "TrackID", "dbo.Tracks", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserArtists", "ArtistID", "dbo.Artists", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Tracks", "ArtistId", "dbo.Artists", "Id", cascadeDelete: true);
        }
    }
}
