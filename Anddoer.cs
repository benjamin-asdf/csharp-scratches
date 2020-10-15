# if false

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

public static class Programm {

    public static void Main(string[] args) {
        Console.WriteLine("==== AndDoer ====\n");


        var t = File.ReadAllText("/tmp/in-file");
        var obj = JsonConvert.DeserializeObject<string[][]>(t);

        foreach (var cmd in  obj) {
            Console.WriteLine($"cmd");
            foreach (var i in cmd) {
                Console.WriteLine(i);
            }

           
        }


    }
}
# endif