using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace MusicApp.Models
{
    public class User : IUser<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FacebookName { get; set; }
        public string FacebookUserId { get; set; }
        public string FacebookAccessToken { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
        public virtual ICollection<Artist> Artists { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<BlacklistedTrack> BlacklistedTracks { get; set; } 
    }
}
