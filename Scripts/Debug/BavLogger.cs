using System.IO;
using System.Runtime.CompilerServices;
using Godot;

namespace BiomeArchitectV3.Scripts.Debug
{
    public static class BavLogger
    {
        private const string GAME_TAG = "BAV3";
        private const int TYPE_WIDTH = -7;
        private const int FILE_WIDTH = -20;
        private const int METHOD_WIDTH = -26;

        public static bool EnableRichLogs { get; set; } = true;
        public static LogType LogLevel { get; set; } = LogType.Debug;



        public static void Error    (string msg, [CallerMemberName] string method = "", [CallerFilePath] string filePath = "") => Log(LogType.Error,    msg, method, filePath);
        public static void Warning  (string msg, [CallerMemberName] string method = "", [CallerFilePath] string filePath = "") => Log(LogType.Warning,  msg, method, filePath);
        public static void Success  (string msg, [CallerMemberName] string method = "", [CallerFilePath] string filePath = "") => Log(LogType.Success,  msg, method, filePath);
        public static void Init     (string msg, [CallerMemberName] string method = "", [CallerFilePath] string filePath = "") => Log(LogType.Init,     msg, method, filePath);
        public static void Info     (string msg, [CallerMemberName] string method = "", [CallerFilePath] string filePath = "") => Log(LogType.Info,     msg, method, filePath);
        public static void Debug    (string msg, [CallerMemberName] string method = "", [CallerFilePath] string filePath = "") => Log(LogType.Debug,    msg, method, filePath);

        

        public static void Log(LogType type, string msg, string method, string filePath)
        {
            if (!ShouldLog(type))
                return;

            string typeText = type.ToString().ToUpperInvariant();
            string file = Path.GetFileNameWithoutExtension(filePath);
            string plainText = $"[{GAME_TAG}] {typeText, TYPE_WIDTH} [{file, FILE_WIDTH}] [{method, METHOD_WIDTH}] - {msg}";

            if(!EnableRichLogs)
            {
                PrintPlain(type, plainText);
                return;
            }

            string colour = GetColour(type);
            string richText = $"[color={colour}]{plainText}[/color]";
            GD.PrintRich(richText);
        }



        private static bool ShouldLog(LogType type)
        {
            return type <= LogLevel;
        }



        private static void PrintPlain(LogType type, string msg)
        {
            switch (type)
            {
                case LogType.Error:
                    GD.PushError(msg);
                    break;
                case LogType.Warning:
                    GD.PushWarning(msg);
                    break;
                default:
                    GD.Print(msg);
                    break;
            }
        }



        private static string GetColour(LogType type)
        {
            return type switch
            {
                LogType.Error => "tomato",
                LogType.Warning => "gold",
                LogType.Success => "yellow_green",
                LogType.Info => "gainsboro",
                LogType.Init => "medium_purple",
                LogType.Debug => "deep_sky_blue",
                _ => "white"
            };
        }
    }
}