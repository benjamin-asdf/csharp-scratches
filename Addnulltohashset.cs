# if false
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;


public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== AddNullToHashset ====\n");

        var hashSet = new HashSet<string>();
        // hashSet.Add("lul");
        // hashSet.Add(Regex.Match("sfdas", "lul").Groups[1].ToString());
        // hashSet.Add("");
        hashSet.Add(null);
        hashSet.Add(null);


        Console.WriteLine(hashSet.Count);
        foreach (var item in hashSet) {
            Console.WriteLine(item);
        }



    }
}
# endif