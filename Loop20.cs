# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== Loop20 ====\n");

        var list = new List<string>() { "bob", "alice", "peter" };

        var x = 0;
        foreach (var e in list) {
            Console.WriteLine(x);
            var istop = x++ < 2;
            Console.WriteLine($"{e} - is top: {istop}");
        }

    }
}
# endif