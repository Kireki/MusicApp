using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MusicApp.Models.Interfaces;

namespace MusicApp.Models
{
    public class BlacklistedTrack : ITrack
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
