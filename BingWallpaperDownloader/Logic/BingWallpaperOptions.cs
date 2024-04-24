namespace BingWallpaperDownloader.Logic {

    public static class BingWallpaperOptions {
        public static bool LogToConsole { get; set; } = true;
        public static bool LogToFile { get; set; } = true;

        public static string TargetFolder { get; set; } = Environment.CurrentDirectory;

        public static int CheckFrequencyHours { get; set; } = 24;

        public static void InitializeOptions() {
            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("LOG_TO_FILE"))) {
                if (bool.TryParse(Environment.GetEnvironmentVariable("LOG_TO_FILE"), out var log_to_file)) {
                    LogToFile = log_to_file;
                } else {
                    Logger.Log($"Invalid option for LOG_TO_FILE: {log_to_file}");
                }
            } else {
                Logger.Log($"Environment variable LOG_TO_FILE not set. Using the value: {LogToFile}");
            }

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("LOG_TO_CONSOLE"))) {
                if (bool.TryParse(Environment.GetEnvironmentVariable("LOG_TO_CONSOLE"), out var log_to_console)) {
                    LogToConsole = log_to_console;
                } else {
                    Logger.LogError($"Invalid option for LOG_TO_CONSOLE: {log_to_console}");
                }
            } else {
                Logger.Log($"Environment variable LOG_TO_CONSOLE not set. Using the value: {LogToConsole}");
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
                Logger.Log($"Environment variable TARGET_FOLDER not set. Using the value: {TargetFolder}");
            }

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CHECK_FREQUENCY_HOURS"))) {
                if (int.TryParse(Environment.GetEnvironmentVariable("CHECK_FREQUENCY_HOURS"), out var check_frequency_hours)) {
                    if (check_frequency_hours < 1 || check_frequency_hours > (30 * 24)) {
                        Logger.LogWarning($"CHECK_FREQUENCY HOURS should be in the range of 1..{30 * 24}");
                    } else {
                        CheckFrequencyHours = check_frequency_hours;
                    }
                }
            } else {
                Logger.Log($"Environment variable CHECK_FREQUENCY_HOURS not set. Using the value: {CheckFrequencyHours} hours");
            }
        }
    }
}