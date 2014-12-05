using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApp.Models
{
    public class Artist
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; } 
        public virtual ICollection<Track> Tracks { get; set; } 
    }
}
