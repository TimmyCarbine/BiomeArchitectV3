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
        public static E_LogType LogLevel { get; set; } = E_LogType.Init;



        public static void Error    (string msg, [CallerMemberName] string method = "", [CallerFilePath] string filePath = "") => Log(E_LogType.Error,    msg, method, filePath);
        public static void Warning  (string msg, [CallerMemberName] string method = "", [CallerFilePath] string filePath = "") => Log(E_LogType.Warning,  msg, method, filePath);
        public static void Success  (string msg, [CallerMemberName] string method = "", [CallerFilePath] string filePath = "") => Log(E_LogType.Success,  msg, method, filePath);
        public static void Init     (string msg, [CallerMemberName] string method = "", [CallerFilePath] string filePath = "") => Log(E_LogType.Init,     msg, method, filePath);
        public static void Info     (string msg, [CallerMemberName] string method = "", [CallerFilePath] string filePath = "") => Log(E_LogType.Info,     msg, method, filePath);
        public static void Debug    (string msg, [CallerMemberName] string method = "", [CallerFilePath] string filePath = "") => Log(E_LogType.Debug,    msg, method, filePath);
        public static void Trace    (string msg, [CallerMemberName] string method = "", [CallerFilePath] string filePath = "") => Log(E_LogType.Trace,    msg, method, filePath);

        

        public static void Log(E_LogType type, string msg, string method, string filePath)
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



        private static bool ShouldLog(E_LogType type)
        {
            return type <= LogLevel;
        }



        private static void PrintPlain(E_LogType type, string msg)
        {
            switch (type)
            {
                case E_LogType.Error:
                    GD.PushError(msg);
                    break;
                case E_LogType.Warning:
                    GD.PushWarning(msg);
                    break;
                default:
                    GD.Print(msg);
                    break;
            }
        }



        private static string GetColour(E_LogType type)
        {
            return type switch
            {
                E_LogType.Error => "tomato",
                E_LogType.Warning => "gold",
                E_LogType.Success => "yellow_green",
                E_LogType.Info => "gainsboro",
                E_LogType.Init => "medium_purple",
                E_LogType.Debug => "deep_sky_blue",
                _ => "white"
            };
        }
    }
}