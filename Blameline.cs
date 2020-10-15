using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

public static class Programm {

    static Regex reg = new Regex(@"^committer \\(.+?\\)$");

    public static void Main(string[] args) {
        Console.WriteLine("==== BlameLine ====\n");

        var line = 4;
        var file = "Anddoer.cs";
        var blameCmd = $"blame --porcelain -L {line},+1 -- {file}";

		var process = Process.Start(
			new ProcessStartInfo("git", blameCmd) {
                WorkingDirectory = Environment.CurrentDirectory,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
        });

		process.WaitForExit();
        var s = process.StandardOutput.ReadToEnd();

        Console.WriteLine(s);


    }
}
