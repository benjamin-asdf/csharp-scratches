# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== SearchdirUp ====\n");

        var path = "/home/benj/best.el";

        foreach (var part in path.Split(new [] {Path.AltDirectorySeparatorChar,Path.DirectorySeparatorChar})) {
            Console.WriteLine(part);
        }







    }
}
# endif