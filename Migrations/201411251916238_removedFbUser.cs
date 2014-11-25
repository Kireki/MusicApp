namespace MusicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedFbUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FacebookUserArtists", "FacebookUser_ID", "dbo.FacebookUsers");
            DropForeignKey("dbo.FacebookUserArtists", "Artist_ID", "dbo.Artists");
            DropForeignKey("dbo.TrackFacebookUsers", "Track_ID", "dbo.Tracks");
            DropForeignKey("dbo.TrackFacebookUsers", "FacebookUser_ID", "dbo.FacebookUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserArtists", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserTracks", "UserID", "dbo.Users");
            DropIndex("dbo.Tracks", new[] { "ArtistID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.FacebookUserArtists", new[] { "FacebookUser_ID" });
            DropIndex("dbo.FacebookUserArtists", new[] { "Artist_ID" });
            DropIndex("dbo.TrackFacebookUsers", new[] { "Track_ID" });
            DropIndex("dbo.TrackFacebookUsers", new[] { "FacebookUser_ID" });
            DropPrimaryKey("dbo.Users");
            CreateTable(
                "dbo.UserArtists",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
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
                        UserID = c.String(nullable: false, maxLength: 128),
                        TrackID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.TrackID })
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Tracks", t => t.TrackID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.TrackID);
            
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "FacebookUser", c => c.String());
            AlterColumn("dbo.Users", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Users", "Id");
            CreateIndex("dbo.Tracks", "ArtistId");
            DropTable("dbo.FacebookUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.FacebookUserArtists");
            DropTable("dbo.TrackFacebookUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TrackFacebookUsers",
                c => new
                    {
                        Track_ID = c.Int(nullable: false),
                        FacebookUser_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Track_ID, t.FacebookUser_ID });
            
            CreateTable(
                "dbo.FacebookUserArtists",
                c => new
                    {
                        FacebookUser_ID = c.Int(nullable: false),
                        Artist_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FacebookUser_ID, t.Artist_ID });
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Age = c.Int(nullable: false),
                        Country = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FacebookUsers",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(),
                        Facebook = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.UserTracks", "TrackID", "dbo.Tracks");
            DropForeignKey("dbo.UserTracks", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserArtists", "ArtistID", "dbo.Artists");
            DropForeignKey("dbo.UserArtists", "UserID", "dbo.Users");
            DropIndex("dbo.UserTracks", new[] { "TrackID" });
            DropIndex("dbo.UserTracks", new[] { "UserID" });
            DropIndex("dbo.UserArtists", new[] { "ArtistID" });
            DropIndex("dbo.UserArtists", new[] { "UserID" });
            DropIndex("dbo.Tracks", new[] { "ArtistId" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Users", "FacebookUser");
            DropColumn("dbo.Users", "Email");
            DropTable("dbo.UserTracks");
            DropTable("dbo.UserArtists");
            AddPrimaryKey("dbo.Users", "ID");
            CreateIndex("dbo.TrackFacebookUsers", "FacebookUser_ID");
            CreateIndex("dbo.TrackFacebookUsers", "Track_ID");
            CreateIndex("dbo.FacebookUserArtists", "Artist_ID");
            CreateIndex("dbo.FacebookUserArtists", "FacebookUser_ID");
            CreateIndex("dbo.AspNetUserLogins", "UserId");
            CreateIndex("dbo.AspNetUserClaims", "UserId");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.AspNetUserRoles", "RoleId");
            CreateIndex("dbo.AspNetUserRoles", "UserId");
            CreateIndex("dbo.AspNetRoles", "Name", unique: true, name: "RoleNameIndex");
            CreateIndex("dbo.Tracks", "ArtistID");
            AddForeignKey("dbo.UserTracks", "UserID", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserArtists", "UserID", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TrackFacebookUsers", "FacebookUser_ID", "dbo.FacebookUsers", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TrackFacebookUsers", "Track_ID", "dbo.Tracks", "ID", cascadeDelete: true);
            AddForeignKey("dbo.FacebookUserArtists", "Artist_ID", "dbo.Artists", "ID", cascadeDelete: true);
            AddForeignKey("dbo.FacebookUserArtists", "FacebookUser_ID", "dbo.FacebookUsers", "ID", cascadeDelete: true);
        }
    }
}
