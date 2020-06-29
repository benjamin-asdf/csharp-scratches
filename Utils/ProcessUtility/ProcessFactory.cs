using System;
using System.Diagnostics;
using System.IO;


public static class ProcessFactory {
    public static Process StartProcessInShell(string fileName, string arguments) {
        return StartProcess(fileName,arguments,useShell: true);
    }

    public static Process StartProcessInWorkingDirectory(string fileName, string arguments, bool useShell = false, bool redirectOutput = false) {
        var workingDirectory = Path.GetFullPath(".");
        fileName = Path.Combine(workingDirectory,fileName);
        return StartProcess(fileName,arguments,useShell,redirectOutput,workingDirectory);
    }

    public static Process StartProcess(string fileName, string arguments, bool useShell = false, bool redirectOutput = false, string workingDirectory = null, string tag = null, bool logOut = false, bool asyncOutput = true) {
        var process = new Process();
        process.StartInfo = ProcessInfoFactory.Create(fileName, arguments, useShell, redirectOutput, workingDirectory);
        var logPrefix = tag == null ? "" : $"[{tag}] ";
        process.Start();
        if(redirectOutput) {
            DigestStdOut(process, l => HandleOutput(LogType.Log, l), asyncOutput);
            DigestStdErr(process, l => HandleOutput(LogType.Error, l), asyncOutput);
        }
        
        void HandleOutput(LogType logType, string line) {
            if (logOut && line != null) {
                Console.WriteLine($"{logType} - {logPrefix}{line}");
            }
        }
        return process;
    }
    
    public static void DigestStdOut(Process process, Action<string> handler, bool async = true) {
        process.OutputDataReceived += (sender, e) => {
            var data = e.Data;
            if (data != null) {
                handler(data);
            }
        };
        if(async) {
            process.BeginOutputReadLine();
        }
    }
    public static void DigestStdErr(Process process, Action<string> handler, bool async = true) {
        process.ErrorDataReceived += (sender, e) => {
            var data = e.Data;
            if (data != null) {
                handler(data);
            }
        };
        if(async) {
            process.BeginErrorReadLine();
        }
    }
    
    public static void DigestOutput(Process process, Action<string> handler, bool async = true) {
        DigestStdOut(process, handler, async);
        DigestStdErr(process, handler, async);
    }
    
    public static void RunProcess(Process process, bool hideArguments = false) {
        var info = process.StartInfo;
        Console.WriteLine("!!! running: " + info.FileName + " " + (hideArguments ? "<arguments hidden>" : info.Arguments));
        process.WaitForExit();
        if (process.ExitCode != 0) {
            throw new Exception($"{info.FileName} exited with exitcode:" + process.ExitCode);
        }
    }
    public static void RunProcess(ProcessStartInfo info, bool hideArguments = false) {
        var p = Process.Start(info);
        RunProcess(p, hideArguments);
    }
}

public static class ProcessInfoFactory {
    
    public static ProcessStartInfo Create(string fileName, string arguements) {
        return Create(fileName,arguements,false,false,null);
    }
    
    public static ProcessStartInfo Create(string fileName, string arguements, bool useShell) {
        return Create(fileName,arguements,useShell,false,null);
    }

    public static ProcessStartInfo Create(string fileName, string arguments, bool useShell, bool redirectOutput) {
        return Create(fileName,arguments,useShell,redirectOutput,null);
    }

    public static ProcessStartInfo Create(string fileName, string arguments, bool useShell, bool redirectOutput, string workingDirectory) {
        
        var info = new ProcessStartInfo {
            FileName  = fileName,
            Arguments = arguments,
            UseShellExecute = useShell && !redirectOutput,
            CreateNoWindow = !(useShell && !redirectOutput),
            RedirectStandardError = redirectOutput,
            RedirectStandardOutput = redirectOutput,
        };
        
        if(!String.IsNullOrEmpty(workingDirectory)) {
            info.WorkingDirectory = workingDirectory;
        }
        return info;
    }

}

public enum LogType {
    //
    // Summary:
    //     LogType used for Errors.
    Error = 0,
    //
    // Summary:
    //     LogType used for Asserts. (These could also indicate an error inside Unity itself.)
    Assert = 1,
    //
    // Summary:
    //     LogType used for Warnings.
    Warning = 2,
    //
    // Summary:
    //     LogType used for regular log messages.
    Log = 3,
    //
    // Summary:
    //     LogType used for Exceptions.
    Exception = 4
}
