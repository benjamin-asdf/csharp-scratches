# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== YieldMachine ====\n");


        foreach (var e in Iterate10()) {
            Console.WriteLine($"foreach.. {e}");
        }

    }


    static IEnumerable<int> Iterate10() {
        for (var i = 0; i < 10; i++) {
            Console.WriteLine($"yielding {i}");
            yield return i;
        }
    }

}
# endif