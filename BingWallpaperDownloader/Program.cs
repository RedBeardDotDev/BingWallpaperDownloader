// See https://aka.ms/new-console-template for more information

using BingWallpaperDownloader.Logic;

// initialise settings based on environment variables.
BWDOptions.InitializeOptions();

while (!BWDOptions.StopRunning) {
    Logger.Log("Starting a Download cycle..");
    await WallpaperUtils.DownloadWallpaperAsync();

    Logger.Log($"Back in wait loop. About to sleep for {BWDOptions.CheckFrequencyHours} hours");
    Thread.Sleep((int)BWDOptions.CheckFrequency.TotalMilliseconds);
}

Logger.Log("Exiting Program");