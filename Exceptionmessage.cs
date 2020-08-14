# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== ExceptionMessage ====\n");
        try {
            hehe();
        } catch (Exception e) {
            Console.WriteLine(e.Message);

        }

    }


    static void hehe () {
        throw new Exception("hfffj");

    }

}
# endif