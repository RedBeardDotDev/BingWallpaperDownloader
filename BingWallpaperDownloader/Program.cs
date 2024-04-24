// See https://aka.ms/new-console-template for more information

using BingWallpaperDownloader.Data;
using BingWallpaperDownloader.Logic;

using var db = new BingDbContext();
await db.Database.EnsureCreatedAsync();

// initialise settings based on environment variables.
await BingWallpaperOptions.InitializeOptionsAsync();

while (!BingWallpaperOptions.StopRunning) {
    await WallpaperUtils.RequestWallpaper();

    Thread.Sleep((int) BingWallpaperOptions.CheckFrequency.TotalMilliseconds);
}

await Logger.LogAsync("Exiting Program");