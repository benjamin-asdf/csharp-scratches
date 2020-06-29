using System;
using System.Collections.Generic;
using System.Diagnostics;

public static class ProcUtil {

    public static List<string> GitCmdAsLines(string args) {
        return OutputAsLines("git",args);
    }
    
    public static List<string> OutputAsLines(string procFileName, string args) {
        var process = Process.Start(
            new ProcessStartInfo(procFileName, args) {
                WorkingDirectory = Environment.CurrentDirectory,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            });

        var ret = new List<string>();
        using (var sr = process.StandardOutput) {
            var line = sr.ReadLine();
            while (!String.IsNullOrEmpty(line)) {
                ret.Add(line);
                line = sr.ReadLine();
            }
        }
        var err = process.StandardError.ReadToEnd();
        process.WaitForExit();
        if (process.ExitCode != 0) {
            throw new Exception($"{procFileName} exited with code {process.ExitCode}\nArgs: {args}\nDirectory: {Environment.CurrentDirectory}\n{err}");
        }
        return ret;
    }
    
}