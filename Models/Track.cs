using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApp.Models
{
    public class Track
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ArtworkUrl { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}