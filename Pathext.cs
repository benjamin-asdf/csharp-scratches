# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== PathExt ====\n");

        var best = "some/path.ext";
        Console.WriteLine(Path.GetExtension(best));
    }
}
# endif