using BingWallpaperDownloader.Logic;

namespace BingWallpaperDownloader {

    public class Program {

        public static async Task Main(string[] args) {
            // initialise settings based on environment variables.
            BWDOptions.InitializeOptions();

            // Effectively an infinite loop, but less likely for Visual Studio to complain than using `while (true) {..}`
            while (!BWDOptions.StopRunning) {
                Logger.Log("Starting a Download cycle..");
                await WallpaperUtils.DownloadWallpaperAsync();

                Logger.Log($"Back in wait loop. About to sleep for {BWDOptions.CheckFrequencyHours} hours");
                Thread.Sleep((int)BWDOptions.CheckFrequency.TotalMilliseconds);
            }

            Logger.Log("Exiting Program");
        }
    }
}