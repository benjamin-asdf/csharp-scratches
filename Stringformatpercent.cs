# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== StringFormatPercent ====\n");

        Console.WriteLine(string.Format("{0:P00}",0.3));
    }
}
# endif