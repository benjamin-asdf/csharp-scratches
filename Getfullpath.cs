# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== GetFullPath ====\n");
        var i = "../../RoslynAnalyzers/BestBannedAnalyzers/bin/BannedApiAnalyzer.CSharp.dll";
        var slnPath = "/home/benj/idlegame/RoslynPlayground/src/";
        Console.WriteLine(Path.Combine(slnPath, "../../"));

        Console.WriteLine(Path.Combine(slnPath,i));

        if (File.Exists(Path.Combine(slnPath,i))) {
            Console.WriteLine("success. file exits.");
        }


        if (File.Exists("../best-lib/.projectile")) {
            Console.WriteLine("sucess.");

        }



    }
}
# endif