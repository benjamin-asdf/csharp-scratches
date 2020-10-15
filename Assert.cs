# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== Assert ====\n");

        bool myCond = false;

        Debug.Assert(myCond,"It was not the cond.");

        // Debug.Assert(false);

        Debug.Assert(true, "f");

        if (false) {

        }



    }
}
# endif