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

        internal static void LogException(string preExceptionInfo, Exception exceptionObject)
        {
            Log($"[Exception Info]" +
                preExceptionInfo +
                $"\n[{DateTime.Now.ToString("h:mm:ss tt")}]: Message: {exceptionObject.Message}" +
                $"\n[{DateTime.Now.ToString("h:mm:ss tt")}]: Source: {exceptionObject.Source}" +
                $"\n[{DateTime.Now.ToString("h:mm:ss tt")}]: InnerException: {(exceptionObject.InnerException != null ? exceptionObject.InnerException.Message : "N/A")}" +
                $"\n[{DateTime.Now.ToString("h:mm:ss tt")}]: Stack Trace: " +
                $"\n{exceptionObject.StackTrace}", LogSeverity.Danger);
        }
    }
}
