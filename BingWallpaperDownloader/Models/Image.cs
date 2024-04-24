using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BingWallpaperDownloader.Logic {

    public class Image {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [JsonProperty("startdate")]
        public string? StartDate { get; set; }

        [JsonProperty("fullstartdate")]
        public string? FullStartDate { get; set; }

        [JsonProperty("enddate")]
        public string? EndDate { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }

        [JsonProperty("urlbase")]
        public string? UrlBase { get; set; }

        [JsonProperty("copyright")]
        public string? Copyright { get; set; }

        [JsonProperty("copyrightlink")]
        public string? CopyrightLink { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("quiz")]
        public string? Quiz { get; set; }

        [JsonProperty("wp")]
        public bool wp { get; set; }

        [JsonProperty("hsh")]
        public string? hsh { get; set; }

        [JsonProperty("drk")]
        public int? drk { get; set; }

        [JsonProperty("top")]
        public int? top { get; set; }

        [JsonProperty("bot")]
        public int? bot { get; set; }

        [JsonProperty("hs")]
        public List<object>? hs { get; set; }

        public override string ToString() {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}