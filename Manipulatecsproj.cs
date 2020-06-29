# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== ManipulateCsProj ====\n");
        var content = File.ReadAllText("best-proj");

        var refPart = @" <ItemGroup>
    <Analyzer Include=""home\benj\idlegame\RoslynAnalzyers\Analyzers\bin\Release\Analyzers.dll"" />
  </ItemGroup>".Replace("\n","\r\n");

        var lines = content.Split("\r\n").ToList();

        lines.Insert(lines.IndexOf("</Project>"),refPart);

        var newContent = "";
        foreach (var l in lines) {
            newContent = $"{newContent}{l}\r\n";
        }

File.WriteAllText("ouot",newContent);


    }
}
# endif