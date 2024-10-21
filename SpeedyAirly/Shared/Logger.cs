using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SpeedyAirly.Interfaces;

namespace SpeedyAirly.Shared
{
    public sealed class Logger :ILogger
    {
        private static readonly Logger instance = new Logger();
        private static readonly object lockObject = new object();
        private readonly string logFilePath = Constants.Log_FilePath;
        

        // Private constructor to prevent direct instantiation
        private Logger()
        {
            // Ensure the log file is created or truncated on application start
            if (!File.Exists(logFilePath))
            {
                using (var stream = File.Create(logFilePath)) { }
            }
        }

        // Public method to get the single instance
        public static Logger Instance
        {
            get
            {
                return instance;
            }
        }

        // Method to log messages to the file
        public void Log(string message)
        {
            lock (lockObject) // Ensures thread safety
            {
                try
                {
                    File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Logging failed: {ex.Message}");
                }
            }
        }
    }

}
