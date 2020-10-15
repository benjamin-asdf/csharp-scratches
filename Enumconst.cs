# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== EnumConst ====\n");

        Console.WriteLine(E.foo);
        Console.WriteLine((int)E.foo);

    }
    const int i = 10;
    enum E {
        foo = i + 2,
    }
}
# endif