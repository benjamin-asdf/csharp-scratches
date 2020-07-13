using System;
using System.Diagnostics;
using System.IO;

public static class Log {
    public static bool verbose;
    public static string LogFile = Path.Combine(Path.GetTempPath(), "analyzer-log");
    static readonly bool logToFile = File.Exists(LogFile);
    public static void Verbose(object obj) {
        if (!verbose) return;
        Info(obj);
    }

    [Conditional("DEBUG")]
    public static void Debug(object message) {
        Console.WriteLine(message);
        LogToFile(message);
    }

    public static void Info(object obj) {
        Console.WriteLine(obj);
        LogToFile(obj);
    }

    public static void Info(params object[] objects) {
        Info(obj: string.Join(" ", objects));
    }

    public static void Error(object obj) {
        Console.Error.WriteLine(obj);
        LogToFile("[Error]: " + obj);
    }

    public static void Stat(string id) {
        var str = "@@STAT " + id;
        Console.WriteLine(str);
        LogToFile(str);
    }

    static void LogToFile(object message) {
        if (logToFile) {
            var tries = 5;
            while (tries > 0) {
                try {
                    File.AppendAllText(LogFile,message + Environment.NewLine);
                    return;
                } finally {
                    tries--;
                }
            }
        }
    }

}
