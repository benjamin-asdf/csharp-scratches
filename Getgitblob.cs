# if false
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== GetGitBlob ====\n");
        var file = ".gitignore";
        var rev = "HEAD";
        CreateBlobFromRevision(file,"HEAD",$"{file}~{rev}");
    }



    static void CreateBlobFromRevision(string path, string rev, string outpath) {

        var args = $"show {rev}:{path}";

        var proc = Process.Start(new ProcessStartInfo("git",args){
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            });

        using (var swriter = new StreamWriter(outpath))
            using (var sreader = proc.StandardOutput) {
                var line = sreader.ReadLine();
                while (!String.IsNullOrEmpty(line)) {
                    swriter.WriteLine(line);
                    line = sreader.ReadLine();
                }
            }
        proc.WaitForExit();
    }


}
# endif