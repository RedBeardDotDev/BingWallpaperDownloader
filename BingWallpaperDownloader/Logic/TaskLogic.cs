namespace BingWallpaperDownloader.Logic {
    public static class TaskLogic {
        public static void RecurringTask(Action action, CancellationToken token) {
            Logger.Log("Running the Recurring Task");

            if (action == null)
                return;
            Task.Run(async () => {
                while (!token.IsCancellationRequested) {
                    Logger.Log("Starting a Recurring Task run");

                    action();
                    await Task.Delay(TimeSpan.FromHours(BingWallpaperOptions.CheckFrequencyHours), token);
                }
            }, token);
        }

    }
}
