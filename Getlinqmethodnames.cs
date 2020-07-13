# if false













using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== GetLinqMethodNames ====\n");


        var methods = typeof(Enumerable).GetMethods();
        foreach (var m in methods) {
            Console.WriteLine(m.Name);
        }

        methods.Max()

        





    }
}
# endif