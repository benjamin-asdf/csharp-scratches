# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== Closures ====\n");

        var iter = GetClosure();
        Console.WriteLine(iter());
        Console.WriteLine(iter());
        Console.WriteLine(iter());
    }

    static Func<int> GetClosure() {
        var iter = 0;
        return () => iter++;
    }



}
# endif