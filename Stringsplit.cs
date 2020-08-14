# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== StringSplit ====\n");

        // var s = "lul;lel;ho";
        // foreach (var part in s.Split(';')) {
        //     Console.WriteLine(part);
        //         // lul
        //         // lel
        //         // ho
        // }

        var s = "lul;;ho";
        foreach (var part in s.Split(';')) {
            Console.WriteLine(part);
            // lul

            // ho
        }


    }
}
# endif