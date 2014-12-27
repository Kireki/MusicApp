using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MusicApp.APIObjectClasses
{
    public class Tag
    {
        public string name { get; set; }
        public string count { get; set; }
        public string url { get; set; }
    }

    public class Attr
    {
        public string artist { get; set; }
    }

    public class Toptags
    {
        public List<Tag> tag { get; set; }
        [JsonProperty(PropertyName = "@attr")]
        public Attr attr { get; set; }
    }

    public class LastFmArtistTags
    {
        public Toptags toptags { get; set; }
    }
}
