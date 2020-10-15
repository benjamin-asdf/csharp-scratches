# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== EnumerateNull ====\n");

        int[] arr = null;
        foreach (var e in arr) {
            Console.WriteLine(e);
        }
    }
}
# endif