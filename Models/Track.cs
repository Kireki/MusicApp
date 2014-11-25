using System.Collections.Generic;

namespace MusicApp.Models
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual ICollection<User> Users { get; set; } 
    }
}