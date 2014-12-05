﻿using System.Data.Entity;
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
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(t => t.Tracks)
                .WithMany(u => u.Users)
                .Map(l => l.MapLeftKey("UserID").MapRightKey("TrackID").ToTable("UserTracks"));

            modelBuilder.Entity<User>()
                .HasMany(a => a.Artists)
                .WithMany(u => u.Users)
                .Map(l => l.MapLeftKey("UserID").MapRightKey("ArtistID").ToTable("UserArtists"));

            modelBuilder.Entity<User>()
                .HasMany(t => t.Tags)
                .WithMany(u => u.Users)
                .Map(l => l.MapLeftKey("UserID").MapRightKey("ArtistID").ToTable("UserTags"));

            modelBuilder.Entity<Artist>()
                .HasMany(t => t.Tags)
                .WithMany(a => a.Artists)
                .Map(l => l.MapLeftKey("ArtistID").MapRightKey("TagName").ToTable("ArtistTags"));
        }
    }
}
