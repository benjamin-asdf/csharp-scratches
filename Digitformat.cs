# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== DigitFormat ====\n");

        var idPrefix = "BEST";
        var id = 43;
        Console.WriteLine($"{idPrefix}{id:D3}");
    }
}
# endif