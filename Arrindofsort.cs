# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== ArrIndOfSort ====\n");

        var arr = new int[] { 1, 10, 3, 2};
        var list = new List<int> { 10, 2 , 3};

        list.Sort((a,b) => {
            return Array.IndexOf(arr,a).CompareTo(Array.IndexOf(arr,b));
        });
        foreach (var item in list) {
            Console.WriteLine(item);
        }

    }
}
# endif