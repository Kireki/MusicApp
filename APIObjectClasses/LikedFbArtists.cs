using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicApp.APIObjectClasses
{
    public class Datum
    {
        public string category { get; set; }
        public string name { get; set; }
        public string created_time { get; set; }
        public string id { get; set; }
    }

    public class Paging
    {
        public string next { get; set; }
    }

    public class LikedFbArtists
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
    }
}