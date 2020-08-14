# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Collections.Immutable;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== CreateImmutableWithNull ====\n");


        var best = ImmutableHashSet.Create<string>(EqualityComparer<string>.Default, null, null);
        Console.WriteLine(best == null);


    }
}
# endif