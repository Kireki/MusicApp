namespace MusicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbRehash : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ArtistTags", "ArtistID", "dbo.Artists");
            DropForeignKey("dbo.ArtistTags", "TagName", "dbo.ArtistTags1");
            DropIndex("dbo.ArtistTags", new[] { "ArtistID" });
            DropIndex("dbo.ArtistTags", new[] { "TagName" });
            RenameColumn(table: "dbo.UserTags", name: "ArtistID", newName: "TagName");
            RenameIndex(table: "dbo.UserTags", name: "IX_ArtistID", newName: "IX_TagName");
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Count = c.Int(nullable: false),
                        Artist_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.Artists", t => t.Artist_Id)
                .Index(t => t.Artist_Id);
            
            DropTable("dbo.ArtistTags");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ArtistTags",
                c => new
                    {
                        ArtistID = c.String(nullable: false, maxLength: 128),
                        TagName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ArtistID, t.TagName });
            
            CreateTable(
                "dbo.ArtistTags1",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
            DropForeignKey("dbo.Tags", "Artist_Id", "dbo.Artists");
            DropIndex("dbo.Tags", new[] { "Artist_Id" });
            DropTable("dbo.Tags");
            RenameIndex(table: "dbo.UserTags", name: "IX_TagName", newName: "IX_ArtistID");
            RenameColumn(table: "dbo.UserTags", name: "TagName", newName: "ArtistID");
            CreateIndex("dbo.ArtistTags", "TagName");
            CreateIndex("dbo.ArtistTags", "ArtistID");
            AddForeignKey("dbo.ArtistTags", "TagName", "dbo.ArtistTags1", "Name", cascadeDelete: true);
            AddForeignKey("dbo.ArtistTags", "ArtistID", "dbo.Artists", "Id", cascadeDelete: true);
        }
    }
}
