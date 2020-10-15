# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== NullTermInput ====\n");

        var path = "/home/benj/repos/git-samples/chekout-hook/changed-files";

        var items = File.ReadAllText(path)
            .Split('\0')
            .ToHashSet();

        foreach (var item in items) {
            Console.WriteLine(item);
        }

    }
}
# endif