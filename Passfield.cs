# if false
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== PassField ====\n");

        bool yes = true;
        bool other = false;

        put(ref other,true);
        if (other) Console.WriteLine("success.");

        put_noRef(yes,false);
        if (yes) Console.WriteLine("success.");

        static void put(ref bool v_place, bool value) {
            v_place = value;
        }

        static void put_noRef(bool v_place, bool value) {
            v_place = value;
        }
    }

}
# endif