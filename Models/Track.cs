﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MusicApp.Models.Interfaces;

namespace MusicApp.Models
{
    public class Track : ITrack
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ArtworkUrl { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}