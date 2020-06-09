# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== ReplaceInList ====\n");


        var list = new List<int> { 1, 2 , 3};

        list.RemoveAt(1);
        list.Insert(1,10);
        foreach (var item in list) {
            Console.WriteLine(item);
        }


    }
}
# endif
