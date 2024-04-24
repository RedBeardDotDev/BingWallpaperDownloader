namespace BingWallpaperDownloader.Logic {

    public static class BingWallpaperOptions {
        public static bool LogToConsole { get; set; } = true;
        public static bool LogToFile { get; set; } = true;

        public static string TargetFolder { get; set; } = Environment.CurrentDirectory;

        public static void InitializeOptions() {
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("LOG_TO_FILE"))) {
                if (bool.TryParse(Environment.GetEnvironmentVariable("LOG_TO_FILE"), out var log_to_file)) {
                    LogToFile = log_to_file;
                } else {
                    Logger.Log($"Invalid option for LOG_TO_FILE: {log_to_file}");
                }
            } else {
                Logger.Log("Environment variable LOG_TO_FILE not set");
            }

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("LOG_TO_CONSOLE"))) {
                if (bool.TryParse(Environment.GetEnvironmentVariable("LOG_TO_CONSOLE"), out var log_to_console)) {
                    LogToConsole = log_to_console;
                } else {
                    Logger.LogError($"Invalid option for LOG_TO_CONSOLE: {log_to_console}");
                }
            } else {
                Logger.Log("Environment variable LOG_TOCONSOLE not set");
            }

            // set target folder
            var target_folder = Environment.GetEnvironmentVariable("TARGET_FOLDER");

            if (!string.IsNullOrEmpty(target_folder)) {
                if (Directory.Exists(target_folder)) {
                    TargetFolder = target_folder;
                } else {
                    Logger.LogError($"Invalid optin for TARGET_FOLDER: {target_folder}");
                }
            } else {
                Logger.Log("Environment variable TARGET_FOLDER not set");
            }
        }
    }
}