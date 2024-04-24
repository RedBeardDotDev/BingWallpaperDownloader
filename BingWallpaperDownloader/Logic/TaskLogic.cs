namespace BingWallpaperDownloader.Logic {
    public static class TaskLogic {
        public static void RecurringTask(Action action, CancellationToken token) {
            if (action == null)
                return;
            Task.Run(async () => {
                while (!token.IsCancellationRequested) {
                    action();
                    await Task.Delay(TimeSpan.FromHours(BingWallpaperOptions.CheckFrequencyHours), token);
                }
            }, token);
        }

    }
}
