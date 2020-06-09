# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== paths ====\n");

        var best = "hehe/lul/best.lul";
        Console.WriteLine($"{Path.GetFileNameWithoutExtension(best)}~HEAD{Path.GetExtension(best)}");


        var _args = "%a %b";
        Console.WriteLine(_args.Replace("%a",$"hehe").Replace("%b","lul"));


    }
}
# endif