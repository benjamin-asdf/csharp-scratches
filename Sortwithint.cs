# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== SortWithInt ====\n");
        var list = new List<string> {"a", "abc", "ab"};
        var list2 = new List<string> {"X", "Xbc", "Xb"};


        var listList = new List<List<string>> {list,list2};
        // list.Sort(s => count(s));

        // static int count(string s) => s.Length;

        // var l = listList.SelectMany(_list => );
        var l = list.SelectMany(element => element.ToCharArray());

        foreach (var item in l) {
            // Console.WriteLine(l);
        }

        var array = new string[] {
                "dot",
                "net",
                "perls"
            };

        var result = array.SelectMany(element => element.ToCharArray());

        // Display letters.
        foreach (char letter in result) {
            Console.WriteLine(letter);
        }

    }



}
# endif