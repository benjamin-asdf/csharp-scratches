# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== SplitNullString ====\n");

        // var s = "hello hehe";
        var s = Console.ReadLine();

        Console.WriteLine(s);
        Console.WriteLine(s.IndexOf('\0'));
        foreach (var m in s.Split('\0')) {
            Console.WriteLine(m);
        }




    }
}
# endif