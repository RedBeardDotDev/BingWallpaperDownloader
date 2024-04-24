// See https://aka.ms/new-console-template for more information

using BingWallpaperDownloader.Logic;

// initialise settings based on environment variables.
BWDOptions.InitializeOptions();

while (!BWDOptions.StopRunning) {
    Logger.Log("Starting a Download cycle..");
    await WallpaperUtils.DownloadWallpaperAsync();

    Thread.Sleep((int)BWDOptions.CheckFrequency.TotalMilliseconds);
}

Logger.Log("Exiting Program");