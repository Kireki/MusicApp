using System.Collections.Generic;

namespace MusicApp.Models
{
    public class Artist
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; } 
        public virtual ICollection<Track> Tracks { get; set; } 
    }
}
