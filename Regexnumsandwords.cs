# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== RegexNumsAndWords ====\n");




        var pattern = @"""(.+?)""";
        var str = @"""best Rider 2""";

        foreach (var item in Regex.Matches(str,pattern)) {
            Console.WriteLine(item.ToString());
        }


        // foreach (var item in Regex.Matches("123",@"\w")) {
        //     Console.WriteLine(item.ToString());
        // }


    }
}
# endif