using System.Collections.Generic;

namespace MusicApp.Models
{
    public class Track
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int ArtistID { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual ICollection<FacebookUser> FacebookUsers { get; set; } 
    }
}