#nullable disable
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public static class ProcessExtensions {
    public static void RunUntilEnd(this Process proc, string procName) {
        if (!proc.HasExited) {
            proc.WaitForExit();
        }

        // if (proc.StartInfo.RedirectStandardOutput) {
        //     LogStdOut(proc, procName);
        // }
        // if (proc.StartInfo.RedirectStandardError) {
        //     LogError(proc, procName);
        // }

        if (proc.ExitCode != 0) {
            Console.Error.WriteLine($"!!!!!!!!!!!!!! Process Failed! Process Name: {procName}");

            throw new Exception();
        }
    }

    public static Process Run(this Process proc) {
        proc.WaitForExit();
        return proc;
    }

    public static void RunAsyncAndLogError(this Process proc, string tag) {
        proc.RunAsync(() => proc.LogError(tag));
    }

    public static void RunAsyncAndLogAllOutput(this Process proc, string tag) {
        proc.RunAsync(() => proc.LogAllOutput(tag));
    }

    public static void RunAsyncAndLogStdOut(this Process proc, string tag) {
        proc.RunAsync(() => proc.LogStdOut(tag));
    }

    public static void RunAsync(this Process proc, Action onComplete = null) {
        proc.EnableRaisingEvents = true;
        proc.Exited += (_, __) => {
            onComplete?.Invoke();
            proc.Dispose();
        };
    }

    public static Process LogAllOutput(this Process proc, string tag) {
        return proc.LogStdOut(tag).LogError(tag);
    }


    public static Process LogError(this Process proc, string tag) {
        var err = proc.StandardError.ReadToEnd();
        Console.WriteLine($"[[[{tag} StdError start]]]\n{err}\n[[[{tag} StdError end]]]");
        return proc;
    }

    public static Process LogStdOut(this Process proc, string tag) {
        var stdOut = proc.StandardOutput.ReadToEnd();
        Console.WriteLine($"[[[{tag} StdOut start]]]\n{stdOut}\n[[[{tag} StdOut end]]]");
        return proc;
    }

}
