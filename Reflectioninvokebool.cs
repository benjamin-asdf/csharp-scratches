# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Reflection;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== ReflectionInvokeBool ====\n");

        var type = typeof(BestClass);

        var info = type.GetMethod("BestMethod") as MethodInfo;
        if (info == null) {
            Console.WriteLine("info is null.");
            return;
        }


        info.Invoke(null, new [] { "1"});





    }


    public static class BestClass {
        public static void BestMethod(bool hi) {
            Console.WriteLine(hi);
        }
    }
}
# endif