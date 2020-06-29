# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;



public static class Programm {

    private static Regex _exclusionRegex;
    static Regex exclusionRegex => _exclusionRegex ?? (_exclusionRegex =  new Regex(@"(Test)|(Plugins)|(WIP)|(Unity)|(Editor)|(Plugins)|(Monkeys)"));


    static HashSet<string> _bestProjectPaths;
    static HashSet<string> BestProjectPaths => _bestProjectPaths ?? InitBestProjPaths();
    static HashSet<string> InitBestProjPaths() {
        _bestProjectPaths = new HashSet<string>();

        var bestNames = new HashSet<string>();
        foreach (var file in Directory.GetFiles("Assets/", "*.asmdef", SearchOption.AllDirectories)) {
            if (!exclusionRegex.IsMatch(file)) {
                bestNames.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

        foreach (var path in Directory.GetFiles(".", "*.csproj", SearchOption.TopDirectoryOnly)) {
            if (bestNames.Contains(Path.GetFileNameWithoutExtension(path))) {
                _bestProjectPaths.Add(path);

            }
        }


        foreach (var item in _bestProjectPaths) {
            Console.WriteLine(item);
        }

        return _bestProjectPaths;

    }



    public static void Main(string[] args) {
        Console.WriteLine("==== GetBestProjects ====\n");

        var dir = Environment.GetEnvironmentVariable("IDLEGAMEDIR");
        Console.WriteLine(dir);
        Environment.CurrentDirectory = dir;

        InitBestProjPaths();

        foreach (var path in BestProjectPaths) {
            DoPatch(path);
        }

    }


    static void DoPatch(string path) {
        var refPart = @"  <ItemGroup>
    <Analyzer Include=""Tools/RoslynAnalyzers/EntityClosureCLI/Analyzers.dll"" />
    <Analyzer Include=""Tools/RoslynAnalyzers/EntityClosureCLI/Microsoft.CodeAnalysis.CSharp.BannedApiAnalyzers.dll"" />
  </ItemGroup>
  <ItemGroup>
    <AdditionalFiles Include=""BannedSymbols.txt"" />
  </ItemGroup>".Replace("\r\n", "\n").Replace("\n", Environment.NewLine);

        var lines = File.ReadLines(path).ToList();
            lines.Insert(lines.IndexOf("</Project>"),refPart);
            File.WriteAllLines(path, lines);
    }


}
# endif
