// See https://aka.ms/new-console-template for more information

using BingWallpaperDownloader.Data;
using BingWallpaperDownloader.Logic;

using var db = new BingDbContext();
await db.Database.EnsureCreatedAsync();

// initialise settings based on environment variables.
BingWallpaperOptions.InitializeOptions();



while (!BingWallpaperOptions.StopRunning) {
    await WallpaperUtils.RequestWallpaper();

    Thread.Sleep((int) BingWallpaperOptions.CheckFrequency.TotalMilliseconds);
}

Logger.Log("Exiting Program");