// See https://aka.ms/new-console-template for more information

using BingWallpaperDownloader.Data;
using BingWallpaperDownloader.Logic;

using var db = new BWDDbContext();
await db.Database.EnsureCreatedAsync();

// initialise settings based on environment variables.
await BWDOptions.InitializeOptionsAsync();

while (!BWDOptions.StopRunning) {
    await Logger.LogAsync("Starting a Download cycle..");
    await WallpaperUtils.DownloadWallpaperAsync();

    Thread.Sleep((int)BWDOptions.CheckFrequency.TotalMilliseconds);
}

await Logger.LogAsync("Exiting Program");