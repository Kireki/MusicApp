using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MusicApp.Models;

namespace MusicApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("AppDbContext")
        {
            //empty
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FacebookUser> FacebookUsers { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Track> Tracks { get; set; }
//
//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<FacebookUser>()
//                .HasMany(c => c.Tracks)
//                .WithMany(i => i.FacebookUsers)
//                .Map(t => t.MapLeftKey("FacebookUserID").MapRightKey("TrackID").ToTable("FacebookUserTracks"));
//
//            modelBuilder.Entity<FacebookUser>()
//                .HasMany(a => a.Artists)
//                .WithMany(f => f.FacebookUsers)
//                .Map(l => l.MapLeftKey("FacebookUserID").MapRightKey("ArtistID").ToTable("FacebookUserArtists"));
//        }
    }
}
