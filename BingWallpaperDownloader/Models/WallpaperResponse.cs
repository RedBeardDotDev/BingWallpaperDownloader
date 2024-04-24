using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BingWallpaperDownloader.Logic {

    public class WallpaperResponse {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [JsonProperty("images")]
        public List<Image>? Images { get; set; }

        [JsonProperty("tooltips")]
        public Tooltips? Tooltips { get; set; }

        public override string ToString() {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}