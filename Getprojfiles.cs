# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

public static class Programm {

    static Regex exclusionRegex = new Regex(@"(Test)|(Plugins)|(WIP)|(Unity)|(Editor)|(Plugins)|(Monkeys)");

    public static void Main(string[] args) {
        Console.WriteLine("==== GetProjFiles ====\n");

        Environment.CurrentDirectory = "/home/benj/idlegame/IdleGame";

        var bestNames = new HashSet<string>();
        var files = Directory.GetFiles("Assets/", "*.asmdef", SearchOption.AllDirectories);
        foreach (var file in files) {
            if (!exclusionRegex.Match(file).Success) {
                bestNames.Add(file);
            }
        }

    }
}
# endif