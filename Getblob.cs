# if false
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== GetBlob ====\n");
        // var file = ".gitignore";
        var file = "file with space";
        CreateBlobFromRevision(file,"HEAD","out-blob");

    }


    static string CreateBlobFromRevision(string path, string rev, string outpath) {
        var proc = Process.Start(new ProcessStartInfo("git",$"show {rev}:\"{path}\""){
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            });

        using (var swriter = new StreamWriter(outpath))
            using (var sreader = proc.StandardOutput) {
                var line = sreader.ReadLine();
                while (line != null) {
                    Console.WriteLine(line);
                    swriter.WriteLine(line);
                    line = sreader.ReadLine();
                }
            }
        proc.WaitForExit();
        if (proc.ExitCode != 0) {
            Console.WriteLine($"exited abnormally with code {proc.ExitCode}");
            Console.WriteLine(proc.ExitCode);
            Console.WriteLine(proc.StandardError.ReadToEnd());
        }


        return outpath;
    }

}
# endif