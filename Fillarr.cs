# if false
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
// using AdjConstRewriter;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== FillArr ====\n");
        var list = new List<int> { 1, 2 , 3};
        var arr = new int[Math.Min(2,list.Count)];
        for (var i = 0; i < arr.Length; i++) {
            arr[i] = list[i];
        }

        var unusedVar = 10;

        // var hi = new AdjConstRewriter.CommonTypes();

        foreach (var item in arr) {
            Console.WriteLine(item);
        }

    }
}
# endif