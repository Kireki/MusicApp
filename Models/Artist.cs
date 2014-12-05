using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApp.Models
{
    public class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FacebookId { get; set; }

        public virtual ICollection<User> Users { get; set; } 
        public virtual ICollection<Track> Tracks { get; set; } 
    }
}
