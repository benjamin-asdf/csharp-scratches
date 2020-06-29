# if false
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== SetArr ====\n");

        var currArr = new int[0][];

        var index = 0;
        var incomming = new int[] {1, 2, 3};

        var list = new List<int[]>();
        foreach (var item in currArr) {
            list.Add(item);
        }

        if (list.Count > index) {
            list.RemoveAt(index);
        }
        list.Insert(index,incomming);
        foreach (var item in list) {
            Console.WriteLine(item);
        }

    }


}
# endif