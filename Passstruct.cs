# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== PassStruct ====\n");

        var best = new Best() {
            num = 10
        };

        method(best);

    }


    static void method(Best best) {
        var _best = best;
        best.num = 100;
        Debug.Assert(_best.num == 10);
    }


    public struct Best {
        public int num;
    }


}
# endif