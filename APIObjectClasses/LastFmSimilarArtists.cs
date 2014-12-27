//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Helpers;
//using Newtonsoft.Json;
//
//namespace MusicApp.APIObjectClasses
//{
//    public class Image
//    {
//        [JsonProperty(PropertyName = "#text")]
//        public string text { get; set; }
//        public string size { get; set; }
//    }
//
//    public class Artist
//    {
//        public string name { get; set; }
//        public string mbid { get; set; }
//        public string match { get; set; }
//        public string url { get; set; }
//        public List<Image> image { get; set; }
//        public string streamable { get; set; }
//    }
//
//    public class Attr
//    {
//        public string artist { get; set; }
//    }
//
//    public class Similarartists
//    {
//        public List<Artist> artist { get; set; }
//        [JsonProperty(PropertyName = "@attr")]
//        public Attr attr { get; set; }
//    }
//
//    public class LastFmSimilarArtists
//    {
//        public Similarartists similarartists { get; set; }
//    }
//}