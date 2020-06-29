# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== StructString ====\n");


        var best = new MyStruct("hi");
        Console.WriteLine(best.myString);



    }


    public struct MyStruct {
        public readonly string myString;

        public MyStruct(string str) {
            this.myString = str;
        }

    }

}
# endif