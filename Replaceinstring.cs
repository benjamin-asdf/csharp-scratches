# if false
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== ReplaceInString ====\n");

        var input = "hello &\nlul";
        var index = input.IndexOf("&") + 1;
        Console.WriteLine(index);

        // var beforePart = input.Substring(0,index);
        // var afterPart = input.Insert(, string value)

        var res = input.Insert(index, "1337");
        Console.WriteLine(res);

    }
}
# endif
