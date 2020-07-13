# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== AddRangeException ====\n");

        var list = new List<int> { 1, 2 , 3};

        var arr = new int[0];

        list.AddRange(null);


    }
}
# endif