# if false

using System;
using System.Linq;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== ProjRegex ====\n");

        var path = "/home/benj/idlegame/IdleGame/Main.csproj";


        var bestProjFile = "/home/benj/idlegame/IdleGame/best-projects";

        var bestProjects = File.ReadAllLines(bestProjFile).ToHashSet();

        foreach (var elem in bestProjects) {
            Console.WriteLine(elem);
        }

        path = Path.GetFileNameWithoutExtension(path);
        Console.WriteLine(path);

        if (bestProjects.Contains(path)) {
            Console.WriteLine("success.");
        }
    }
}
# endif