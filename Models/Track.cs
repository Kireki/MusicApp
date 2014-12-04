using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApp.Models
{
    public class Track
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual ICollection<User> Users { get; set; } 
    }
}