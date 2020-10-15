# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== StructDefaultEquals ====\n");

        var a = new S {s = "fa", a = true};
        var b = new S {s = "fa", a = true};

        var _set = new HashSet<S>();
        _set.Add(a);
        _set.Add(b);

        Console.WriteLine(_set.Count());


    }


    struct S {
        public string s;
        public bool a;
    }

}
# endif