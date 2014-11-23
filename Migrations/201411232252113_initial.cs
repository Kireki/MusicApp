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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FacebookUserArtists",
                c => new
                    {
                        FacebookUser_ID = c.Int(nullable: false),
                        Artist_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FacebookUser_ID, t.Artist_ID })
                .ForeignKey("dbo.FacebookUsers", t => t.FacebookUser_ID, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.Artist_ID, cascadeDelete: true)
                .Index(t => t.FacebookUser_ID)
                .Index(t => t.Artist_ID);
            
            CreateTable(
                "dbo.TrackFacebookUsers",
                c => new
                    {
                        Track_ID = c.Int(nullable: false),
                        FacebookUser_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Track_ID, t.FacebookUser_ID })
                .ForeignKey("dbo.Tracks", t => t.Track_ID, cascadeDelete: true)
                .ForeignKey("dbo.FacebookUsers", t => t.FacebookUser_ID, cascadeDelete: true)
                .Index(t => t.Track_ID)
                .Index(t => t.FacebookUser_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TrackFacebookUsers", "FacebookUser_ID", "dbo.FacebookUsers");
            DropForeignKey("dbo.TrackFacebookUsers", "Track_ID", "dbo.Tracks");
            DropForeignKey("dbo.Tracks", "ArtistID", "dbo.Artists");
            DropForeignKey("dbo.FacebookUserArtists", "Artist_ID", "dbo.Artists");
            DropForeignKey("dbo.FacebookUserArtists", "FacebookUser_ID", "dbo.FacebookUsers");
            DropIndex("dbo.TrackFacebookUsers", new[] { "FacebookUser_ID" });
            DropIndex("dbo.TrackFacebookUsers", new[] { "Track_ID" });
            DropIndex("dbo.FacebookUserArtists", new[] { "Artist_ID" });
            DropIndex("dbo.FacebookUserArtists", new[] { "FacebookUser_ID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Tracks", new[] { "ArtistID" });
            DropTable("dbo.TrackFacebookUsers");
            DropTable("dbo.FacebookUserArtists");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Users");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tracks");
            DropTable("dbo.FacebookUsers");
            DropTable("dbo.Artists");
        }
    }
}
