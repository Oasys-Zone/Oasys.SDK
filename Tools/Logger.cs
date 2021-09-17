using System;
using System.Reflection;

namespace Oasys.SDK.Tools
{
    /// <summary>
    /// Standard console logger.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Logs the given data to the console.
        /// </summary>
        /// <param name="dataToLog"></param>
        /// <param name="severity"></param>
        public static void Log(object dataToLog, LogSeverity severity = LogSeverity.Neutral)
        {
            var consoleColor = Console.ForegroundColor;

            switch (severity)
            {
                case LogSeverity.Neutral:
                    consoleColor = ConsoleColor.Cyan;
                    break;
                case LogSeverity.Warning:
                    consoleColor = ConsoleColor.Yellow;
                    break;
                case LogSeverity.Danger:
                    consoleColor = ConsoleColor.Red;
                    break;
                default:
                    consoleColor = ConsoleColor.White;
                    break;
            }

            Console.ForegroundColor = consoleColor;

            Console.WriteLine($"[{DateTime.Now.ToString("h:mm:ss tt")} - {Assembly.GetCallingAssembly().GetName().Name}]: {dataToLog}");
        }
    }
}
