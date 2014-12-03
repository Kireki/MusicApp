namespace MusicApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class storingAccessTokens : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FacebookAccessToken", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "FacebookAccessToken");
        }
    }
}