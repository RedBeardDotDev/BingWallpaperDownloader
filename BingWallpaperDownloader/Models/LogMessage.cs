using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text;

namespace BingWallpaperDownloader.Models {

    public class LogMessage {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? Id { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string? Filename { get; set; }
        public string? MethodName { get; set; }
        public int? LineNum { get; set; }
        public int? ColNum { get; set; }
        public string? Level { get; set; }
        public string? Message { get; set; }

        public LogMessage() {
            var st = new StackTrace(true);
            var frames = st.GetFrames();
            StackFrame? found = null;

            foreach (var frame in frames) {
                var method = frame.GetMethod();
                var file = frame.GetFileName();

                if (file == null) {
                    continue;
                }

                var fi = new FileInfo(file);

                if (!fi.Name.EndsWith("Logger.cs")
                    && !fi.Name.EndsWith("LogMessage.cs")
                    && !fi.Name.EndsWith("DebugMessageService.cs")) {
                    found = frame;
                    break;
                }
            }

            Filename = found?.GetFileName();
            MethodName = found?.GetMethod()?.Name;
            LineNum = found?.GetFileLineNumber();
            ColNum = found?.GetFileColumnNumber();
        }

        public LogMessage(LogLevel level, string message) {
            var found = new LogMessage();

            Filename = found.Filename;
            MethodName = found.MethodName;
            LineNum = found.LineNum;
            ColNum = found.ColNum;

            Level = level.ToString();
            Message = message;
        }

        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}");

            if (Level == $"{LogLevel.Debug}") {
                sb.Append($"{Level?.ToUpper()}: {Filename}:{MethodName}:{LineNum}:{ColNum}:");
            }

            sb.Append($" {Message}");

            return sb.ToString();
        }
    }
}