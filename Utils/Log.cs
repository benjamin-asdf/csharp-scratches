using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Threading;

public static class Log {
    public static bool verbose;
    public static string LogFile = Path.Combine(Path.GetTempPath(), "analyzer-log");
    static readonly bool logToFile = File.Exists(LogFile);
    static readonly bool StatLogEnabled = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI"));
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
        if (StatLogEnabled) {
            var str = "@@STAT " + id;
            Console.WriteLine(str);
            LogToFile(str);
        }
    }

    public static StatRegion StatRegion(string id) {
        return new StatRegion(id);
    }

    static void LogToFile(object message) {
        if (logToFile) {
            LogToFile(LogFile,message);
        }
    }

    static void LogToFile(string file, object message) {
        var tries = 5;
        while (true) {
            try {
                File.AppendAllText(file, message + Environment.NewLine);
                return;
            } catch {
                if (--tries < 0) {
                    throw;
                } else {
                    Thread.Sleep(50);
                }
            }
        }
    }

    public class TraceSession : IDisposable {
        readonly string fileName;
        public bool Valid {get; private set;}
        public bool FlushOnDispose {get; set;}

        public TraceSession(string fileName) {
            this.fileName = fileName;
            Valid = true;
        }

        public void Log(object message) {
            LogToFile(fileName,message);
        }

        public void Dispose() {
            Valid = false;
            if (File.Exists(fileName)) {
                if (FlushOnDispose) {
                    Info(File.ReadAllText(fileName));
                }
                File.Delete(fileName);
            }
        }
    }

}

public struct StatRegion : IDisposable {
    readonly string id;
    readonly Stopwatch sw;

    public StatRegion(string id) {
        this.id = id;
        sw = Stopwatch.StartNew();
    }

    public void Dispose() {
        Log.Info(id + $" took {sw.ElapsedMilliseconds}ms");
    }
}
