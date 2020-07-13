# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== ParamsNull ====\n");

        M("lul", "hehee");
        M(null);

    }

    static void M(params string[] args) {
        if (args == null) return; //needed.
        foreach (var arg in args) {
            Console.WriteLine(arg);
        }
    }

}
# endif