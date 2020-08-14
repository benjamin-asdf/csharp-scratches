# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== ConcatLists ====\n");

        var list = new List<int> { 1, 2 , 3};
        var other = new List<int> { 10, 20 , 30};
        var third = new List<int> { 100, 200 , 300};


        foreach (var e in list.Concat(third).Concat(other)) {
            Console.WriteLine(e);
        }



    }
}
# endif