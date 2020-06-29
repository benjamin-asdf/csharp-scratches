# nullable disable
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public static class Git {
    public static void Checkout(string arguments, bool silent = false) {
        if(silent) {
            runGitCommandSilent("checkout", arguments, asyncOutput: false);
        } else {
            runGitCommandSync("checkout", arguments, asyncOutput: false);
        }
    }

    public static void Reset() {
        Task.Run(() => {
            runGitCommandSilent("add", "-A");
            runGitCommandSilent("reset", "--hard --quiet");
        });
    }

    public static void Pull(string arguments = null) {
        runGitCommandSync("pull", arguments);
    }

    public static void Clean(string arguments = null) {
        runGitCommandSync("clean", arguments);
    }


    public static void runGitCommandSync(string command, string arguments, bool asyncOutput = true) {
        using (var gitProcess = runGitCommand(command, arguments, asyncOutput: asyncOutput)) {
            gitProcess.Run().LogError($"git {command}");
        }
    }

    static void runGitCommandSilent(string command, string arguments, bool asyncOutput = true) {
        using (var gitProcess = runGitCommand(command, arguments, redirectOutput: false, asyncOutput: asyncOutput)) {
            gitProcess.WaitForExit();
        }
    }

    public static string GetCommitHash() {
        using (var cmd = runGitCommand("log", " -n 1 --pretty=format:%h")) {
            return cmd.Run().StandardOutput.ReadToEnd();
        }
    }

    public static string GetBranchName() {
        using (var cmd = runGitCommand("rev-parse", "--abbrev-ref HEAD")) {
            return cmd.Run().StandardOutput.ReadToEnd();
        }
    }

    static Process runGitCommand(string command, string arguments, bool redirectOutput = true, bool asyncOutput = true) {
        return ProcessFactory.StartProcess("git", $"{command} {arguments}", redirectOutput: redirectOutput, asyncOutput: asyncOutput);
    }
}
