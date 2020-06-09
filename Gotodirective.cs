# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== GotoDirective ====\n");



        if (true) {
            
            goto Success;
        }


        Console.WriteLine("hello");
        return;


      Success:
        Console.WriteLine("success");

    }
}
# endif