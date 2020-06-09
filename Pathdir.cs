# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== PathDir ====\n");
            string bestDir = Path.Combine("/best/hee/", "Editor", "AssetTools");
            Console.WriteLine(Path.Combine(bestDir, "bestFile"));

    }
}
# endif
