using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BingWallpaperDownloader.Logic {

    public class Tooltips {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [JsonProperty("loading")]
        public string? Loading { get; set; }

        [JsonProperty("previous")]
        public string? Previous { get; set; }

        [JsonProperty("next")]
        public string? Next { get; set; }

        [JsonProperty("walle")]
        public string? Walle { get; set; }

        [JsonProperty("walls")]
        public string? Walls { get; set; }

        public override string ToString() {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}