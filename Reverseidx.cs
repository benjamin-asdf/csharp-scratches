# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== ReverseIdx ====\n");

        var list = new List<int> { 1, 2 , 3};

        var idx = 0;
        foreach (var item in list) {
            Console.WriteLine(item.ToString());
            var i = idx;
            Console.WriteLine($"idx: {i}");
            Console.WriteLine($"reversed idx: {list.Count - 1 - i}");
            idx++;
        }


    }
}
# endif