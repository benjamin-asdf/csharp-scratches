# if false
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public static class Programm {

    static string template = "/home/benj/idlegame/IdleGame/csc-rsp-template";

    private static Regex _exclusionRegex;
    static Regex exclusionRegex => _exclusionRegex ?? (_exclusionRegex =  new Regex(@"(Test)|(Plugins)|(WIP)|(Unity)|(Editor)|(Plugins)|(Monkeys)"));


    static HashSet<string> InitBestAsmdefPaths() {
        var bestPaths = new HashSet<string>();
        return bestPaths;
    }

    public static void Main(string[] args) {
        Console.WriteLine("==== CscSetter ====\n");
        Environment.CurrentDirectory = Environment.GetEnvironmentVariable("IDLEGAMEDIR");

        foreach (var file in Directory.GetFiles("Assets/", "*.asmdef", SearchOption.AllDirectories)) {
            // if (!exclusionRegex.IsMatch(file)) {
                Console.WriteLine(Path.GetDirectoryName(file));
                File.Copy(template, Path.Combine(Path.GetDirectoryName(file),"csc.rsp"),true);
            // }
        }
    }



}
# endif