# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== MutableStruct ====\n");

        var b = new Building {
            h = new StringHolder { str = "abs" }
        };
        var a = b;
        b.h.str = null;

        Console.WriteLine(a.h.str);


    }


    public struct Building {
        public StringHolder h;
    }

    public class StringHolder {
        public string str;
    }
}
# endif