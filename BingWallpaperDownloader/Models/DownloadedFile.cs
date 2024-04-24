using BingWallpaperDownloader.Logic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BingWallpaperDownloader.Models {

    public class DownloadedFile {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public Guid Guid { get; set; } = Guid.NewGuid();

        public DateTimeOffset WhenDownloaded { get; set; } = DateTimeOffset.Now;
        public WallpaperResponse? WallpaperResponse { get; set; }

        public string? Filename { get; set; }
    }
}