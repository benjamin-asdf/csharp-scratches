# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== ForeachNull ====\n");

        foreach (var arr in overrides) {

        }

    }

    static ViewManagerArr[] _overrides;
    static ViewManagerArr[] overrides => _overrides;

    [Serializable]
    class ViewManagerArr {
        public int[] arr;
    }
}
# endif