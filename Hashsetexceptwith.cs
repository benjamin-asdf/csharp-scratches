# if false
using System;
using System.Collections.Generic;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== HashSetExceptWith ====\n");

        var paths = new HashSet<string>{"hello", "lul"};
        paths.ExceptWith(new [] {"hello"});
        foreach (var item in paths) {
            Console.WriteLine(item);
        }

        // ==== HashSetExceptWith ====

        //          lul

    }
}
# endif