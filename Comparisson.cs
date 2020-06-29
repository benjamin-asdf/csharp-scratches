# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== Comparisson ====\n");

        var list = new List<int> { 1, 10 , 3};

        // list.Sort((a,b) => a.CompareTo(b));

        Console.WriteLine(10.CompareTo(10));


        foreach (var item in list) {
            Console.WriteLine(item);
        }


    }
}
# endif