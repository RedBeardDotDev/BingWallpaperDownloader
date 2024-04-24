using BingWallpaperDownloader.Logic;
using BingWallpaperDownloader.Models;
using Microsoft.EntityFrameworkCore;

namespace BingWallpaperDownloader.Data {

    public class BWDDbContext : DbContext {
        public DbSet<Image> Images => Set<Image>();
        public DbSet<Tooltips> Tooltips => Set<Tooltips>();
        public DbSet<WallpaperResponse> WallpaperResponses => Set<WallpaperResponse>();
        public DbSet<LogMessage> LogMessages => Set<LogMessage>();
        public DbSet<DownloadedFile> DownloadedFiles => Set<DownloadedFile>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            optionsBuilder.UseSqlite(@$"Data Source={appdata}\BingWallpaper.db");
        }
    }
}