using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApp.Models
{
    public class FacebookUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Facebook { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }
        public virtual ICollection<Artist> Artists { get; set; } 
    }
}
