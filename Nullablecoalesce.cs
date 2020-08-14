# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== NullableCoalesce ====\n");

        int? num = 10;

        Console.WriteLine(num ?? 11);
        Console.WriteLine(num.Value);


    }



}
# endif