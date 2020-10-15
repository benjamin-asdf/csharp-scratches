# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== EnumCast ====\n");

        Console.WriteLine((Second)First.fem);

    }


    enum First {
        fa,
        fu,
        fem
    }

    enum Second {
        faa,
        fuu
    }

}
# endif