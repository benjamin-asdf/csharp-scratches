# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== YieldBreak ====\n");

        foreach (var item in F()) {
            Console.WriteLine(item);
        }
    }

    static List<int> list = new List<int> {
        2
    };

    public static IEnumerable<int> F() {
        var cnt = list.Count();
        for (var i = 0; i < Math.Max(10,cnt); i++) {
            Console.WriteLine($"state {i}");
            if (i > 10) {
                Console.WriteLine("greater than needed");
            } else {
                if (list.Count() <= i) {
                    list.Add(i);
                }
                if (list.Count() > i) {
                    yield return i;
                }
            }


        }


    }

}
# endif