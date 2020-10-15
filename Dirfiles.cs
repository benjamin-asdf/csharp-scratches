# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== DirFiles ====\n");

        foreach (var f in Directory.GetFiles("/home/benj/repos/csharp/csharp-scratches/", "*cs")) {
            Console.WriteLine(f);
        }

    }
}
# endif