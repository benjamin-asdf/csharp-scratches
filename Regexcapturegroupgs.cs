# if false
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== RegexCaptureGroupgs ====\n");


        // string pattern = @"(\w+)\s(\1)";
        // string input = "He said that that was the the correct answer.";
        // foreach (Match match in Regex.Matches(input, pattern, RegexOptions.IgnoreCase))
        //     Console.WriteLine("Duplicate '{0}' found at positions {1} and {2}.",
        //                       match.Groups[1].Value, match.Groups[1].Index, match.Groups[2].Index);


        // string pattern = @"hello\s+(\w+)";

        // Console.WriteLine(Regex.Match("hello mofo",@"hello\s(\w+)"));


        // foreach (Match match in Regex.Matches("hello mofo hello hehe", pattern)) {
        //     foreach (var g in match.Groups) {
        //         Console.WriteLine($"match value: {match.Value}.. group: {g.ToString()}");
        //     }
        // }

        // for (var i = 0; i < matches.Count; i++) {
        //     Console.WriteLine($"match {i}");
        //     for (var j = 0; j < matches[i].Groups.Count; j++) {
        //         Console.WriteLine($"group {j} {matches[i].Groups[j].ToString()}");
        //     }
        // }

        var input = File.ReadAllLines("/home/benj/repos/unity-sample/UnitySample/Assets/Editor/difftoolspecfile");


        var programName = "";
        var _args = "";

        foreach (var line in File.ReadAllLines("/home/benj/repos/unity-sample/UnitySample/Assets/Editor/difftoolspecfile")) {
            if (line.StartsWith("#")) continue;

            var matches = Regex.Matches(line,@"^\* use ""(.*)""");
            if (matches.Count == 1) {

                programName = matches[0].Groups[1].ToString();
                _args = line.Substring(matches[0].Groups[0].Length);

                Console.WriteLine(programName);
                Console.WriteLine("args:");
                Console.WriteLine(line.Substring(matches[0].Groups[0].Length + 1));
                break;
            }
        }


        if (String.IsNullOrEmpty(programName)) {
            Console.WriteLine("Error diff tool file.");
        }

        // if windows

        var first = programName.Replace("%programs%",@"C:\Program Files");
        // Console.WriteLine(first);
        var second = programName.Replace("%programs%",@"C:\Program Files (x86)");
        // Console.WriteLine(second);

        // substring

        // Console.WriteLine(str.Substring(programName.Length - 1));


    }



}
# endif